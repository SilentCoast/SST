![image](https://github.com/SilentCoast/SST/assets/94042423/c7b52275-f9a3-4b08-b488-150df9d815cd)

---

To check Import/Export functionality you can use files provided at [SST/TestPoints](SST/TestPoints)

---

Known problems:

-graph is not visually updated till you drag or zoom it (to work around that added text to show user that they need to drag or zoom)

-Even though OutOfMemoryException is cathed, it still crashes the programm

---
**Regarding Revit**

After quick research I found out that there is a core library: *Autodesk.Revit*, and additional libraries, such: *RevitAPIUI*, *RevitAPIUIAutomation*, *RevitTestFramework*, which all depend on the core one

So for starters I would go with *Autodesk.Revit* and if some inconvinience occur, I'd look into other libs.

But of course before working with it additional research is required

