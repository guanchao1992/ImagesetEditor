ImagesetEditor
==============

开发语言：C#

Imageset 编辑器

使用它来快速创建&编辑 imageset，以及自定义文本格式导出。

Update
==============

2014.02.23 0.1.0 -> 0.1.1

添加当图片移出工作区域的停靠功能

添加"始终显示图片边框"选项记录至项目中

修改工作区域边框的绘制顺序

Preview
==============

![preview](http://www.frimin.com/imageset.jpg "preview")

###纯文本导出：
    "image01" 228,0 76,76
    "image02" 0,0 76,76
    "image03" 52,356 76,76
    "image04" 408,228 76,76
    "image05" 408,152 76,76
    "image06" 332,228 76,76
    "image07" 152,76 76,76
    "image08" 76,76 76,76
    "image09" 0,76 76,76
    "image10" 152,0 76,76
    "image11" 76,0 76,76
    "image12" 0,152 128,128
    "image13" 380,0 76,76
    "image14" 128,304 76,76
    "image15" 332,152 76,76
    "image16" 52,280 76,76
    "image17" 204,280 76,76
    "image18" 380,76 76,76
    "image19" 304,0 76,76
    "image20" 304,76 76,76
    "image21" 228,76 76,76
    "image22" 128,228 76,76
    "image23" 128,152 76,76
    "image24" 204,152 128,128
    
###Xml导出：
    <?xml version="1.0" encoding="utf-8"?>
    <Imageset>
      <Image Name="image01" XPos="228" YPos="0" Width="76" Height="76" />
      <Image Name="image02" XPos="0" YPos="0" Width="76" Height="76" />
      <Image Name="image03" XPos="52" YPos="356" Width="76" Height="76" />
      <Image Name="image04" XPos="408" YPos="228" Width="76" Height="76" />
      <Image Name="image05" XPos="408" YPos="152" Width="76" Height="76" />
      <Image Name="image06" XPos="332" YPos="228" Width="76" Height="76" />
      <Image Name="image07" XPos="152" YPos="76" Width="76" Height="76" />
      <Image Name="image08" XPos="76" YPos="76" Width="76" Height="76" />
      <Image Name="image09" XPos="0" YPos="76" Width="76" Height="76" />
      <Image Name="image10" XPos="152" YPos="0" Width="76" Height="76" />
      <Image Name="image11" XPos="76" YPos="0" Width="76" Height="76" />
      <Image Name="image12" XPos="0" YPos="152" Width="128" Height="128" />
      <Image Name="image13" XPos="380" YPos="0" Width="76" Height="76" />
      <Image Name="image14" XPos="128" YPos="304" Width="76" Height="76" />
      <Image Name="image15" XPos="332" YPos="152" Width="76" Height="76" />
      <Image Name="image16" XPos="52" YPos="280" Width="76" Height="76" />
      <Image Name="image17" XPos="204" YPos="280" Width="76" Height="76" />
      <Image Name="image18" XPos="380" YPos="76" Width="76" Height="76" />
      <Image Name="image19" XPos="304" YPos="0" Width="76" Height="76" />
      <Image Name="image20" XPos="304" YPos="76" Width="76" Height="76" />
      <Image Name="image21" XPos="228" YPos="76" Width="76" Height="76" />
      <Image Name="image22" XPos="128" YPos="228" Width="76" Height="76" />
      <Image Name="image23" XPos="128" YPos="152" Width="76" Height="76" />
      <Image Name="image24" XPos="204" YPos="152" Width="128" Height="128" />
    </Imageset>
