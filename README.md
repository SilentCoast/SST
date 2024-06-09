# SST - 2D Points Graph Viewer

![image](https://github.com/SilentCoast/SST/assets/94042423/c7b52275-f9a3-4b08-b488-150df9d815cd)

Welcome to **SST**! This WPF application allows you to visualize and manage 2D points on a graph. You can import and export points using XML files, with support for multiple file operations. Additionally, you can add new points through the UI, view a list of all existing points, and see them plotted on a graph.

## Features

- Visualize Points: Display 2D points on a graph.
- Import Points: Import points from one or multiple XML files.
- Export Points: Export points to one or multiple XML files.
- Add Points: Add new points through the user interface.
- List Points: View a list of all existing points.
- User-Friendly Interface: Easy-to-use graphical interface with WPF.

## Installation

To get started with SST, follow these steps:

1. **Clone the repository:**

    ```bash
    git clone https://github.com/yourusername/SST.git
    cd SST
    ```

2. **Open the Solution:**
   - Open the `.sln` file in Visual Studio.

3. **Build the Solution:**
   - Build the solution in Visual Studio to restore the dependencies.

## Usage

1. **Run the application:**
   - Start the application by running it from Visual Studio or by executing the compiled `.exe` file.

2. **Import Points:**
   - Click on the "Import" button in the UI.
   - Select one or multiple XML files containing the points you wish to import.

3. **Export Points:**
   - Click on the "Export" button in the UI.
   - Choose the destination directory and the number of files you want to export the points to.

4. **Add Points:**
   - Use the "Add Point" feature in the UI to add new points manually.

5. **View Points:**
   - The imported and added points will be displayed in the list and plotted on the 2D graph in the UI.

## XML File Format

The XML files should be formatted as follows:

```xml
<points>
  <point x="2.1" y="2" />
  <!-- Add more points as needed -->
</points>
```


