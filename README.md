ImagesetEditor
==============

开发语言：C#

Imageset 编辑器

使用它来快速创建&编辑 imageset，以及自定义文本格式导出。

Preview
==============

![preview](http://www.frimin.com/imageset.jpg "preview")

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

Updates
==============

2014.02.25 0.1.2 -> 0.1.3

添加选择多个图片进行停靠的功能

2014.02.24 0.1.1 -> 0.1.2

添加图片对齐功能，在选择对齐位置时会有辅助线段显示

添加"修改工作区颜色"选项并且记录至项目中

将画布尺寸的设置由选择一些固定的数值修改为输入任意数值应用

修复没有显示停靠箭头的情况下鼠标移动到其位置依然会修改光标为"手型"的情况

2014.02.23 0.1.0 -> 0.1.1

添加当图片移出工作区域的停靠功能

添加"始终显示图片边框"选项记录至项目中

修改工作区域边框的绘制顺序