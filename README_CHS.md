图片集编辑器
==============

开发于 Visual Studio 2013 C#

**请把工作目录设置为 "..\..\" **

概述
==============

困扰于合并大量的图片？你可以考虑使用下这个工具

* 添加图片快速合并它们并且导出位置

* 自动排版相同高度或者宽度的图片

* 保存至项目文件

支持输入格式：BMP/JPG/PNG

支持输出的格式：PNG

![preview](http://www.frimin.com/imageset_en.jpg "preview")

###纯文本导出：
    "image01" 228,0 76,76
    "image02" 0,0 76,76
    "image03" 52,356 76,76
    "image04" 408,228 76,76
    "image05" 408,152 76,76
	...
    
###Xml导出：
    <?xml version="1.0" encoding="utf-8"?>
    <Imageset>
      <Image Name="image01" XPos="228" YPos="0" Width="76" Height="76" />
      <Image Name="image02" XPos="0" YPos="0" Width="76" Height="76" />
      <Image Name="image03" XPos="52" YPos="356" Width="76" Height="76" />
      <Image Name="image04" XPos="408" YPos="228" Width="76" Height="76" />
      <Image Name="image05" XPos="408" YPos="152" Width="76" Height="76" />
	  ...
    </Imageset>
    
###重新编写输出接口以自定义文本格式输出
    class MyExport : IImagesetExport
    {
        void OnExportBegin(ICanvas canvas)
        {
            // 输出图片信息之前被调用
        }

        void OnExportImage(IImage image)
        {
            // 输出图片信息被调用
            // IImage 接口可获取图片的 位置、尺寸、文件路径、名称
        }

        string OnExportEnd()
        {
            // 输出图片信息结束时被调用
            // 返回的字符串为合并的图片的导出路径，如果返回 null 或 "" 则不导出。
        }
        
        MyExport()
        {
            // ...
        }
    }
    
    // ...
    
    ImagesetEditControl.Export(new MyExport());

如何使用
==============

1.在左侧 **图片组** 视图菜单中点击 **添加** 按钮来添加一个或多个图片。

2.在左侧列表框中或者右侧画布视图中选择一个或者多个图片来移动他们，如果所选的图片与其他图片重叠，则会出现八个方向的停靠箭头。光标移动到箭头上会显示要停靠到的位置。如果你选中了多个图片，则以它们组成一个更大的矩形来计算重叠。

3.选中单个图片时，画布视图的工具栏中的属性文本框可用，你可以编辑图片的名称和位置。

4.**文件** 菜单中 **保存项目** 来保存你的项目至 imageset 文件，如果项目中的图片文件处于项目文件的同目录或者子目录下时，存储的路径是相对路径。

5.使用 **文件** 菜单中 **导出** 来导出特定格式。

许可
==============

###BSD许可证
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

更新
==============

**2014.03.11** 0.2.3 -> 0.2.4

修复点击图片时不能选择的问题

增强自动排版功能，支持仅宽相同或者高相同的图片排版

**2014.03.09** 0.2.2 -> 0.2.3

修复排序后没有更新选择项的顺序的BUG

修复一些显示BUG

新增自动排版功能，在选择多个相同尺寸的图片时画布视图工具栏会出现自动排版选项

**2014.03.08** 0.2.1 -> 0.2.2

修复 **导出至上一次的位置** 菜单项的错误

添加 **最近的文件** 菜单项

**2014.03.07** 0.2.0 -> 0.2.1

修复在打开项目之后再新建项目的情况下，选择保存项目会直接覆盖上一个项目而不是另外提示保存**（严重BUG）**

修复在停靠尺寸比较小的图片时箭头会重叠的情况

添加 Ctrl 键 + 鼠标左键在画布中选中/取消选中图片的功能

添加使用方向键来移动图片

**2014.02.25** 0.1.3 -> 0.2.0

添加多国语言

**2014.02.25** 0.1.2 -> 0.1.3

添加选择多个图片进行停靠的功能

**2014.02.24** 0.1.1 -> 0.1.2

添加图片对齐功能，在选择对齐位置时会有辅助线段显示

添加"修改工作区颜色"选项并且记录至项目中

将画布尺寸的设置由选择一些固定的数值修改为输入任意数值应用

修复没有显示停靠箭头的情况下鼠标移动到其位置依然会修改光标为"手型"的情况

**2014.02.23** 0.1.0 -> 0.1.1

添加当图片移出工作区域的停靠功能

添加"始终显示图片边框"选项记录至项目中

修改工作区域边框的绘制顺序
