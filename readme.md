# O2 Editor Tools

`O2 Editor Tools` is a Unity Editor tool-kit designed to streamline your development process. This toolkit was created as I encountered the need for new tools. Some of the scripts are hard-coded, but I believe that’s not an issue.

## Features

### **Hierarchy Highlighter**
This tool allows you to color objects with specific tags in Unity's hierarchy window. It also enables you to modify their font properties and alignment for better organization.

![image](https://github.com/user-attachments/assets/c546f84a-08d2-40bc-b677-bb8663934feb)
![image](https://github.com/user-attachments/assets/82f76410-1a24-4b0e-803a-c8e7d2d27e28)

### **Scene Utility**
The Scene Utility editor window helps you quickly switch between scenes. Regardless of which scene you're currently in, it ensures that the selected setup scene opens automatically as soon as Unity enters play mode. It's a simple but effective tool, especially when frequently switching between scenes.
![image](https://github.com/user-attachments/assets/1fb55e47-b308-4567-a41e-da20a65b15bd)

### **Validator Utility**
Validator Utility displays fields marked with attributes in the editor, alerting you when they are not assigned. In some cases, it automatically assigns unfilled fields. Additionally, it detects GameObjects with missing scripts, listing them one by one. You can choose to delete these objects if necessary. Furthermore, you can define the validation scope with various options, exclude namespaces, and use the `IgnoreValidationAttribute` to prevent validation of specific items.
![image](https://github.com/user-attachments/assets/c3cf7075-f5d6-42b4-b83d-4a7e88a3fec8)
![image](https://github.com/user-attachments/assets/73ff3a73-e5ea-4ff4-829b-8d8f2cb86649)

### **Script Draft**
With Script Draft, you can pre-define various script templates. By right-clicking in the project file explorer, you can quickly create a new `.cs` script based on the selected template. In the background, it uses the O2 Script Builder: [o2scriptbuilderlink]
![image](https://github.com/user-attachments/assets/344f468f-a293-4d50-8f17-6296de071aca)
![image](https://github.com/user-attachments/assets/5ed5ebab-c94b-453f-8e09-c09482b4548c)


### **Quick Fab**
Quick Fab allows you to categorize models for tasks like level design. You can access these categories by right-clicking in the hierarchy window and easily select models from the categories you've defined. Once selected, you can quickly add them to your scene.
![image](https://github.com/user-attachments/assets/6316e3d1-7513-42a6-803b-f2059bcc2b2d)
![image](https://github.com/user-attachments/assets/bc97c0fe-c980-4d8c-a76a-5ec1ebda6e1d)

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
