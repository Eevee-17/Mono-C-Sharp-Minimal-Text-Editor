using System;
using System.Windows.Forms;
using System.Drawing;

public class TextEditor : Form {
	[STAThread]
	public static void Main() {
		Application.Run(new TextEditor());
	}
	
	private RichTextBox text_area;
	private MainMenu menu;
	string save_file_name = "";
	string[] args = Environment.GetCommandLineArgs();
	public TextEditor() {
		Text = "Text Editor";
		ShowIcon = false;
		ClientSize = new System.Drawing.Size(896, 512);
		text_area = new RichTextBox();
		text_area.Dock = DockStyle.Fill;
		text_area.Font = new Font("Courier New", 10);
		text_area.TextChanged += new EventHandler(UpdatedText);
		menu = new MainMenu();
		menu.MenuItems.Add(new MenuItem("Open", OnOpenFileClick));
		menu.MenuItems.Add(new MenuItem("Save As", OnSaveAsClick));
		menu.MenuItems.Add(new MenuItem("Save", OnSaveClick, Shortcut.CtrlS));
		Controls.Add(text_area);
		this.Menu = menu;
		if(args.Length > 1) {
			text_area.LoadFile(args[1], RichTextBoxStreamType.PlainText);
			save_file_name = args[1];
			Text = save_file_name;
		}
	}
	
	void OnOpenFileClick(object sender, System.EventArgs e) {
		OpenFileDialog file_dialog = new OpenFileDialog();
		file_dialog.ShowDialog();
		if(file_dialog.FileName.Trim() != string.Empty) {
			text_area.LoadFile(file_dialog.FileName, RichTextBoxStreamType.PlainText);
			save_file_name = file_dialog.FileName;
			Text = save_file_name;
		}
		file_dialog.Dispose();
		file_dialog = null;
	}
	
	void OnSaveAsClick(object sender, System.EventArgs e) {
		SaveFileDialog file_dialog = new SaveFileDialog();
		file_dialog.ShowDialog();
		if(file_dialog.FileName.Trim() != string.Empty) {
			text_area.SaveFile(file_dialog.FileName, RichTextBoxStreamType.PlainText);
			save_file_name = file_dialog.FileName;
			Text = save_file_name;
		}
		file_dialog.Dispose();
		file_dialog = null;
	}
	void OnSaveClick(object sender, System.EventArgs e) {
		if(save_file_name != string.Empty) {
			text_area.SaveFile(save_file_name, RichTextBoxStreamType.PlainText);
			Text = save_file_name;
		} else {
			OnSaveAsClick(sender, e);
		}
	}
	void UpdatedText(object sender, System.EventArgs e) {
		Text = "*" + save_file_name;
	}
}