ImagesetEditor
==============

[README_CHS](https://github.com/frimin/ImagesetEditor/blob/master/README_CHS.md) (Chinese simplified version readme file)

Development in Visual Studio 2013 C#

Please set the working directory to "..\\..\\"

Overview
==============

Be confused in merging lots of images? You might consider using this tool.

* Fast merge images and easily to export image's data.

* Auto typesetting same height or width images.

* Save to project file.

Support input formats: BMP / JPG / PNG

Supports output formats: PNG

![preview](http://www.frimin.com/imageset_en.jpg "preview")

###Plain text output：
    "image01" 228,0 76,76
    "image02" 0,0 76,76
    "image03" 52,356 76,76
    "image04" 408,228 76,76
    "image05" 408,152 76,76
	...
    
###Xml output：
    <?xml version="1.0" encoding="utf-8"?>
    <Imageset>
      <Image Name="image01" XPos="228" YPos="0" Width="76" Height="76" />
      <Image Name="image02" XPos="0" YPos="0" Width="76" Height="76" />
      <Image Name="image03" XPos="52" YPos="356" Width="76" Height="76" />
      <Image Name="image04" XPos="408" YPos="228" Width="76" Height="76" />
      <Image Name="image05" XPos="408" YPos="152" Width="76" Height="76" />
	  ...
    </Imageset>

###Rewrite the output interface to customize the output text format
    class MyExport : IImagesetExport
    {
        void OnExportBegin(ICanvas canvas)
        {
            // Be called before the output image information.
        }

        string OnExportEnd()
        {
            // Be called after the output image information.
            // Returns a string for the merger picture export path, If it returns null or "" not exported.
        }
        
        public void OnExportGroupBegin(GroupExpression expression)
        {
            
        }

        public void OnExportGroupEnd()
        {

        }
        
        void OnExportImage(IImage image)
        {
            // Called on export image information
        }
        
        MyExport()
        {
            // ...
        }
    }
    
    // ...
    
    ImagesetEditControl.Export(new MyExport());

How to use
==============

1.Add one or more images on **Images** - **Add** menu item.

2.You can select images in list view or canvas. Drag images to move in canvas. If the selected image overlap with other images, you can see eight directions arrow.Move the cursor to the arrow will appear a line , It is the destination location.If you selected multiple images, seen as a larger rectangle to calculate the overlap.

3.When you select a single picture, you can edit the name and position of the picture on property text box.

4.Click **File** - **Save** menu item to save your project. If image files in the same directory of the project file or subdirectory, relative path is stored.

5.Click **File** - **Export**  to export to file.

License
==============
###BSD License
    * Copyright (c) 2014, Frimin < buzichang@vip.qq.com >. All rights reserved.
    *
    * Redistribution and use in source and binary forms, with or without
    * modification, are permitted provided that the following conditions are met:
    *
    *     * Redistributions of source code must retain the above copyright
    *       notice, this list of conditions and the following disclaimer.
    *     * Redistributions in binary form must reproduce the above copyright
    *       notice, this list of conditions and the following disclaimer in the
    *       documentation and/or other materials provided with the distribution.
    *     * Neither the name Frimin, nor the names of its contributors may be used
    *       to endorse or promote products derived from this software without 
    *       specific prior written permission.
    *
    * THIS SOFTWARE IS PROVIDED BY FRIMIN AND CONTRIBUTORS "AS IS" AND ANY
    * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
    * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    * DISCLAIMED. IN NO EVENT SHALL FRIMIN AND CONTRIBUTORS BE LIABLE FOR ANY
    * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
    * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
    * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
    * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
    * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
    * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    
Updates
==============

**April 12, 2014** 0.2.4 -> 0.2.5

Adding right mouse button to drag the canvas.

Adding drag image files to added from Explorer.

Adding create image group.

Adding exit the program will to ask save project.

**March 9, 2014** 0.2.1 -> 0.2.3

Fixed sort bugs.

Fixed some display bugs.

Adding auto typesetting, to select some of the same size image to view typesetting options.

**March 8, 2014** 0.2.1 -> 0.2.2

Fixed **Exported to last Position** menu item's bug.

Adding **Recent Files** menu item.

**March 7, 2014** 0.2.0 -> 0.2.1

Fixed save project bug. **(Serious bug)**

Fixed when selected small image drawing overlapping arrows.

Adding press the **Ctrl** key + left mouse button to select / unselect images.

Adding press the arrow keys to move images.

**Feb 25, 2014** 0.1.3 -> 0.2.0

Adding multiple languages.
