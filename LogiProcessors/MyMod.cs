using PiTung;
using UnityEngine;
using System;
using PiTung.Components;
using PiTung.Console;

namespace LogiProcessors
{
	public class LogiProcessors : Mod
	{
		public override string Name => "LogiProcessors";
		public override string PackageName => "Ryan.LogiProcessors";
		public override string Author => "Ryan";
		public override Version ModVersion => new Version("0.1.0");

		//public static string[] CodeGlobal = {"jmp 0"};

		public static string CodeLocationGlobal = "";
		public override void BeforePatch()
		{
			var LP22 = PrefabBuilder.Custom(() => CPUFP.createCube(2, 2))
				.WithInput(CPUFP.getPegPos(2, 2, "L", 0.5f, 0f), CPUFP.getPegQuat("L"), "Reset Processor / Update Code")
				.WithInput(CPUFP.getPegPos(2, 2, "L", -0.5f, 0f), CPUFP.getPegQuat("L"), "Update Code Location")
				.WithInput(CPUFP.getPegPos(2, 2, "R", 0.5f, 0f), CPUFP.getPegQuat("R"), "Clock")
				.WithInput(CPUFP.getPegPos(2, 2, "B", -0.5f, 0f), CPUFP.getPegQuat("B"), "Input 0")
				.WithInput(CPUFP.getPegPos(2, 2, "B", 0.5f, 0f), CPUFP.getPegQuat("B"), "Input 1")
				.WithOutput(CPUFP.getPegPos(2, 2, "F", 0.5f, 0f), CPUFP.getPegQuat("F"), "Output 0")
				.WithOutput(CPUFP.getPegPos(2, 2, "F", -0.5f, 0f), CPUFP.getPegQuat("F"), "Output 1");
			ComponentRegistry.CreateNew<Processer>("LogiProcessors22", "Processor 2 Bit IO", LP22);

			var LP44 = PrefabBuilder.Custom(() => CPUFP.createCube(4, 4))
				.WithInput(CPUFP.getPegPos(4, 4, "L", 0.5f, 0f), CPUFP.getPegQuat("L"), "Reset Processor / Update Code")
				.WithInput(CPUFP.getPegPos(4, 4, "L", -0.5f, 0f), CPUFP.getPegQuat("L"), "Update Code Location")
				.WithInput(CPUFP.getPegPos(4, 4, "R", 1.5f, 0f), CPUFP.getPegQuat("R"), "Clock")
				.WithInput(CPUFP.getPegPos(4, 4, "B", -1.5f, 0f), CPUFP.getPegQuat("B"), "Input 0")
				.WithInput(CPUFP.getPegPos(4, 4, "B", -0.5f, 0f), CPUFP.getPegQuat("B"), "Input 1")
				.WithInput(CPUFP.getPegPos(4, 4, "B", 0.5f, 0f), CPUFP.getPegQuat("B"), "Input 2")
				.WithInput(CPUFP.getPegPos(4, 4, "B", 1.5f, 0f), CPUFP.getPegQuat("B"), "Input 3")
				.WithOutput(CPUFP.getPegPos(4, 4, "F", 1.5f, 0f), CPUFP.getPegQuat("F"), "Output 0")
				.WithOutput(CPUFP.getPegPos(4, 4, "F", 0.5f, 0f), CPUFP.getPegQuat("F"), "Output 1")
				.WithOutput(CPUFP.getPegPos(4, 4, "F", -0.5f, 0f), CPUFP.getPegQuat("F"), "Output 2")
				.WithOutput(CPUFP.getPegPos(4, 4, "F", -1.5f, 0f), CPUFP.getPegQuat("F"), "Output 3");
			ComponentRegistry.CreateNew<Processer>("LogiProcessors44", "Processor 4 Bit IO", LP44);

			var LP88 = PrefabBuilder.Custom(() => CPUFP.createCube(8, 8))
				.WithInput(CPUFP.getPegPos(8, 8, "L", 0.5f, 0f), CPUFP.getPegQuat("L"), "Reset Processor / Update Code")
				.WithInput(CPUFP.getPegPos(8, 8, "L", -0.5f, 0f), CPUFP.getPegQuat("L"), "Update Code Loc")
				.WithInput(CPUFP.getPegPos(8, 8, "R", 3.5f, 0f), CPUFP.getPegQuat("R"), "Clock")
				.WithInput(CPUFP.getPegPos(8, 8, "B", -3.5f, 0f), CPUFP.getPegQuat("B"), "Input 0")
				.WithInput(CPUFP.getPegPos(8, 8, "B", -2.5f, 0f), CPUFP.getPegQuat("B"), "Input 1")
				.WithInput(CPUFP.getPegPos(8, 8, "B", -1.5f, 0f), CPUFP.getPegQuat("B"), "Input 2")
				.WithInput(CPUFP.getPegPos(8, 8, "B", -0.5f, 0f), CPUFP.getPegQuat("B"), "Input 3")
				.WithInput(CPUFP.getPegPos(8, 8, "B", 0.5f, 0f), CPUFP.getPegQuat("B"), "Input 4")
				.WithInput(CPUFP.getPegPos(8, 8, "B", 1.5f, 0f), CPUFP.getPegQuat("B"), "Input 5")
				.WithInput(CPUFP.getPegPos(8, 8, "B", 2.5f, 0f), CPUFP.getPegQuat("B"), "Input 6")
				.WithInput(CPUFP.getPegPos(8, 8, "B", 3.5f, 0f), CPUFP.getPegQuat("B"), "Input 7")
				.WithOutput(CPUFP.getPegPos(8, 8, "F", 3.5f, 0f), CPUFP.getPegQuat("F"), "Output 0")
				.WithOutput(CPUFP.getPegPos(8, 8, "F", 2.5f, 0f), CPUFP.getPegQuat("F"), "Output 1")
				.WithOutput(CPUFP.getPegPos(8, 8, "F", 1.5f, 0f), CPUFP.getPegQuat("F"), "Output 2")
				.WithOutput(CPUFP.getPegPos(8, 8, "F", 0.5f, 0f), CPUFP.getPegQuat("F"), "Output 3")
				.WithOutput(CPUFP.getPegPos(8, 8, "F", -0.5f, 0f), CPUFP.getPegQuat("F"), "Output 4")
				.WithOutput(CPUFP.getPegPos(8, 8, "F", -1.5f, 0f), CPUFP.getPegQuat("F"), "Output 5")
				.WithOutput(CPUFP.getPegPos(8, 8, "F", -2.5f, 0f), CPUFP.getPegQuat("F"), "Output 6")
				.WithOutput(CPUFP.getPegPos(8, 8, "F", -3.5f, 0f), CPUFP.getPegQuat("F"), "Output 7");
			ComponentRegistry.CreateNew<Processer>("LogiProcessors88", "Processor 8 Bit IO", LP88);

			var LP1616 = PrefabBuilder.Custom(() => CPUFP.createCube(8, 17))
				.WithInput(CPUFP.getPegPos(8, 17, "L", 0.5f, 0f), CPUFP.getPegQuat("L"), "Update Code / Reset Processer")
				.WithInput(CPUFP.getPegPos(8, 17, "L", -0.5f, 0f), CPUFP.getPegQuat("L"), "Update Code Loc")
				.WithInput(CPUFP.getPegPos(8, 17, "R", 3.5f, 0f), CPUFP.getPegQuat("R"), "Clock")
				.WithInput(CPUFP.getPegPos(8, 17, "B", 1f, 0f), CPUFP.getPegQuat("B"), "Port 0 Input 0")
				.WithInput(CPUFP.getPegPos(8, 17, "B", 2f, 0f), CPUFP.getPegQuat("B"), "Port 0 Input 1")
				.WithInput(CPUFP.getPegPos(8, 17, "B", 3f, 0f), CPUFP.getPegQuat("B"), "Port 0 Input 2")
				.WithInput(CPUFP.getPegPos(8, 17, "B", 4f, 0f), CPUFP.getPegQuat("B"), "Port 0 Input 3")
				.WithInput(CPUFP.getPegPos(8, 17, "B", 5f, 0f), CPUFP.getPegQuat("B"), "Port 0 Input 4")
				.WithInput(CPUFP.getPegPos(8, 17, "B", 6f, 0f), CPUFP.getPegQuat("B"), "Port 0 Input 5")
				.WithInput(CPUFP.getPegPos(8, 17, "B", 7f, 0f), CPUFP.getPegQuat("B"), "Port 0 Input 6")
				.WithInput(CPUFP.getPegPos(8, 17, "B", 8f, 0f), CPUFP.getPegQuat("B"), "Port 0 Input 7")
				.WithInput(CPUFP.getPegPos(8, 17, "B", -8f, 0f), CPUFP.getPegQuat("B"), "Port 1 Input 0")
				.WithInput(CPUFP.getPegPos(8, 17, "B", -7f, 0f), CPUFP.getPegQuat("B"), "Port 1 Input 1")
				.WithInput(CPUFP.getPegPos(8, 17, "B", -6f, 0f), CPUFP.getPegQuat("B"), "Port 1 Input 2")
				.WithInput(CPUFP.getPegPos(8, 17, "B", -5f, 0f), CPUFP.getPegQuat("B"), "Port 1 Input 3")
				.WithInput(CPUFP.getPegPos(8, 17, "B", -4f, 0f), CPUFP.getPegQuat("B"), "Port 1 Input 4")
				.WithInput(CPUFP.getPegPos(8, 17, "B", -3f, 0f), CPUFP.getPegQuat("B"), "Port 1 Input 5")
				.WithInput(CPUFP.getPegPos(8, 17, "B", -2f, 0f), CPUFP.getPegQuat("B"), "Port 1 Input 6")
				.WithInput(CPUFP.getPegPos(8, 17, "B", -1f, 0f), CPUFP.getPegQuat("B"), "Port 1 Input 7")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", -1f, 0f), CPUFP.getPegQuat("F"), "Port 0 Output 0")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", -2f, 0f), CPUFP.getPegQuat("F"), "Port 0 Output 1")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", -3f, 0f), CPUFP.getPegQuat("F"), "Port 0 Output 2")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", -4f, 0f), CPUFP.getPegQuat("F"), "Port 0 Output 3")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", -5f, 0f), CPUFP.getPegQuat("F"), "Port 0 Output 4")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", -6f, 0f), CPUFP.getPegQuat("F"), "Port 0 Output 5")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", -7f, 0f), CPUFP.getPegQuat("F"), "Port 0 Output 6")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", -8f, 0f), CPUFP.getPegQuat("F"), "Port 0 Output 7")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", 8f, 0f), CPUFP.getPegQuat("F"), "Port 1 Output 0")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", 7f, 0f), CPUFP.getPegQuat("F"), "Port 1 Output 1")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", 6f, 0f), CPUFP.getPegQuat("F"), "Port 1 Output 2")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", 5f, 0f), CPUFP.getPegQuat("F"), "Port 1 Output 3")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", 4f, 0f), CPUFP.getPegQuat("F"), "Port 1 Output 4")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", 3f, 0f), CPUFP.getPegQuat("F"), "Port 1 Output 5")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", 2f, 0f), CPUFP.getPegQuat("F"), "Port 1 Output 6")
				.WithOutput(CPUFP.getPegPos(8, 17, "F", 1f, 0f), CPUFP.getPegQuat("F"), "Port 1 Output 7");
			ComponentRegistry.CreateNew<Processer>("LogiProcessors1616", "Processor 2 Port IO", LP1616);

			Shell.RegisterCommand<setCodeLoc>();
		}
	}
}
