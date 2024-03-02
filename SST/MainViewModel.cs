using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PropertyChanged;
using SST.Classes;
using SSTLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace SST
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public bool IsProcessing {  get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public ObservableCollection<ThePoint> Points { get; set; }
        public PlotModel plotModel { get; set; }
        private readonly IPointsSerializer<ThePoint> PointsSerializer;
        private readonly ScatterSeries scatterSeries;
        private readonly string CacheFileName;
        public MainViewModel(IPointsSerializer<ThePoint> pointsSerializer, string cacheFileName)
        {
            PointsSerializer = pointsSerializer;
            CacheFileName = cacheFileName;
            try
            {
                var doc = XmlWorker.Load(CacheFileName);
                Points = new ObservableCollection<ThePoint>(PointsSerializer.Deserialize(doc));
            }
            catch (FileNotFoundException)
            {
                //there will be no file at first start of the app, so just ignore it
                Points = new ObservableCollection<ThePoint>();
            }
            catch(Exception e) 
            {
                MessageBox.Show($"Cannot load data\nInner Exception message: {e.Message}");
                Points = new ObservableCollection<ThePoint>();
            }

            scatterSeries = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
            };

            AddPointsToScatterSeries(Points);
            CreatePlotModel();
            
            Points.CollectionChanged += Points_CollectionChanged;
        }
        
        public RelayCommand AddPoint => addPoint ?? (addPoint = new RelayCommand(p =>
        {
            try
            {
                Points.Add(new ThePoint(X, Y));
            }
            catch(ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
        }));
        public RelayCommand Import => import ?? (import = new RelayCommand(async p =>
        {
            IsProcessing = true;
            bool success = true;
            OpenFileDialog openFileDialog = CreateOpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                if (MessageBox.Show("Your current data will be overwritten, proceed?", "",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    List<ThePoint> points = new List<ThePoint>();
                    
                    await Task.Run(() =>
                    {
                        Parallel.ForEach(openFileDialog.FileNames,
                            new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount },
                            (filename, loopState) =>
                        {
                            try
                            {
                                var doc = XmlWorker.Load(filename);
                                var somePoints = PointsSerializer.Deserialize(doc);
                                lock (points)
                                {
                                    points.AddRange(somePoints);
                                }
                            }
                            catch (OutOfMemoryException)
                            {
                                MessageBox.Show("We are not yet able to process that many points, try selecting smaller files or less files at a time");
                                success = false;
                                loopState.Break();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show($"Could not proccess data in file ({filename}): {e.Message}");
                                success = false;
                            }
                        });
                    });
                    
                    
                    if (success)
                    {
                        Points.CollectionChanged -= Points_CollectionChanged;
                        Points = new ObservableCollection<ThePoint>(points);
                        AddPointsToScatterSeries(Points);
                        Points.CollectionChanged += Points_CollectionChanged;
                        MessageBox.Show("Import finished");
                    }
                }
            }
            IsProcessing = false;
        }));
        public RelayCommand Export => export ?? (export = new RelayCommand(async p =>
        {
            IsProcessing = true;
            bool success = true;   
            OpenFileDialog openFileDialog = CreateOpenFileDialog();

            if(openFileDialog.ShowDialog() == true)
            {
                if(MessageBox.Show("Chosen files will be overwritten, proceed?", "", 
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    await Task.Run(() =>
                    {
                        Parallel.ForEach(openFileDialog.FileNames,
                            new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                            (filename,loopState) =>
                        {
                            try
                            {
                                XmlWorker.Save(filename, PointsSerializer.Serialize(Points));
                            }
                            catch (OutOfMemoryException)
                            {
                                MessageBox.Show("We are not yet able to process that many points, try selecting smaller files or less files at a time");
                                success = false;
                                loopState.Break();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                                success = false;
                            }
                        });
                    });
                    if (success)
                    {
                        MessageBox.Show("Export finished");
                    }
                }
            }
            IsProcessing = false;
        }));
        private OpenFileDialog CreateOpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "xml";
            openFileDialog.Filter = "XML files (*.xml) | *.xml";
            openFileDialog.Multiselect = true;
            return openFileDialog;
        }
        private void CreatePlotModel()
        {
            plotModel = new PlotModel();
            plotModel.Series.Add(scatterSeries);

            plotModel.Axes.Add(new LinearAxis()
            {
                PositionAtZeroCrossing = true,
                Position = AxisPosition.Bottom,
                Minimum = MultiplyValue(ThePoint.XLowConstraint),
                Maximum = MultiplyValue(ThePoint.XHighConstraint)
            });
            plotModel.Axes.Add(new LinearAxis()
            {
                PositionAtZeroCrossing = true,
                Position = AxisPosition.Left,
                Minimum = MultiplyValue(ThePoint.YLowConstraint),
                Maximum = MultiplyValue(ThePoint.YHighConstraint)
            });
            double MultiplyValue(double value)
            {
                return value * 1.5;
            }
        }
        private void Points_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            AddPointsToScatterSeries(e.NewItems);
        }
        private void AddPointsToScatterSeries(IList items)
        {
            foreach (var item in items)
            {
                ThePoint point = (ThePoint)item;
                //TODO: graph is not visually updated till you drag or zoom it
                scatterSeries.Points.Add(new ScatterPoint(point.X, point.Y, 5, 433));
            }
        }
        public void OnExit()
        {
            XmlWorker.Save(CacheFileName, PointsSerializer.Serialize(Points));
        }
        RelayCommand addPoint;
        RelayCommand import;
        RelayCommand export;
    }
}
