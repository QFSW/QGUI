# QGUI ![](https://img.shields.io/github/issues-closed-raw/QFSW/QGUI.svg?color=51c414) ![](https://img.shields.io/github/issues-raw/QFSW/QGUI.svg?color=c41414&style=popout)
A simple but effective IMGUI library for when EditorGUILayout is unavailable. It was created for internal use within my various tools and plugins 
 - [QuantumConsole](https://assetstore.unity.com/packages/tools/utilities/quantum-console-128881)

QGUI provides the `LayoutController` that eases working with Rects by being able to reserve the demanded space within the rect, automatically applying padding and offsets for you

### Example Usage

The following is an example of using QGUI's `LayoutController` to emulate a `HorizontalLayoutGroup` where `EditorGUILayout` is unavailable

`EditorGUILayout`

```csharp
EditorGUILayout.BeginHorizontal();
val1 = EditorGUILayout.Toggle(label1, val1, GUILayout.Width(100));
val2 = EditorGUILayout.Toggle(label2, val2);
EditorGUILayout.EndHorizontal();
```

`QGUI`

```csharp
LayoutController layout = new LayoutController(rect);
val1 = EditorGUI.Toggle(layout.ReserveHorizontal(100), label1, val1);
val2 = EditorGUI.Toggle(layout.CurrentRect, label2, val2);
```

### Installation via Package Manager

#### 2019.3+
Starting with Unity 2019.3, the package manager UI has support for git packages

Click the `+` to add a new git package and add `https://github.com/QFSW/QGUI.git` as the source

#### 2018.3 - 2019.2
To install via package manager, add the file `Packages/manifest.json` and add the following line to the `"dependencies"`
```
"com.qfsw.qgui": "https://github.com/QFSW/QGUI.git"
```
Your file should end up like this 
```
{
  "dependencies": {
    "com.qfsw.qgui": "https://github.com/QFSW/QGUI.git",
    ...
  },
}
```
