# O2 Editor Tools

`O2 Editor Tools` is a Unity Editor tool-kit designed to streamline your development process. This toolkit was created as I encountered the need for new tools. Some of the scripts are hard-coded, but I believe that’s not an issue.

## Features

### **Hierarchy Highlighter**
This tool allows you to color objects with specific tags in Unity's hierarchy window. It also enables you to modify their font properties and alignment for better organization.

### **Scene Utility**
The Scene Utility editor window helps you quickly switch between scenes. Regardless of which scene you're currently in, it ensures that the selected setup scene opens automatically as soon as Unity enters play mode. It's a simple but effective tool, especially when frequently switching between scenes.

### **Validator Utility**
Validator Utility displays fields marked with attributes in the editor, alerting you when they are not assigned. In some cases, it automatically assigns unfilled fields. Additionally, it detects GameObjects with missing scripts, listing them one by one. You can choose to delete these objects if necessary. Furthermore, you can define the validation scope with various options, exclude namespaces, and use the `IgnoreValidationAttribute` to prevent validation of specific items.

### **Script Draft**
With Script Draft, you can pre-define various script templates. By right-clicking in the project file explorer, you can quickly create a new `.cs` script based on the selected template. In the background, it uses the O2 Script Builder: [o2scriptbuilderlink]

### **Quick Fab**
Quick Fab allows you to categorize models for tasks like level design. You can access these categories by right-clicking in the hierarchy window and easily select models from the categories you've defined. Once selected, you can quickly add them to your scene.

## Installation

1. Download the `O2 Editor Tools` Unity package.
2. Import it into your Unity project.
3. Access the tools and settings from **O2/Settings** in the top context menu in the editor. From there, you can enable/disable and modify the options you want.

## Usage

- Use **Hierarchy Highlighter** to color-code objects in the hierarchy and adjust their font settings.
- Access **Scene Utility** to quickly switch and load the setup scene when entering play mode.
- Use **Validator Utility** to manage unassigned fields and missing scripts.
- Utilize **Script Draft** to create predefined script templates with ease.
- Categorize models and access them quickly with **Quick Fab**.
