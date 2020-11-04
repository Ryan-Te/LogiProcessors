using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using PiTung;
using PiTung.Components;
using PiTung.Console;
using System.Runtime.InteropServices;
using UnityEngineInternal;

namespace LogiProcessers
{
	class Processer : UpdateHandler
	{
		//[SaveThis]
		//public string CodeLocation { get; set; } = "";
		[SaveThis]
		public byte Line { get; set; }
		[SaveThis]
		public byte[] Registers { get; set; }
		[SaveThis]
		public byte[] Memory { get; set; }

		[SaveThis]
		public string codeloc { get; set; }
		public string[] code { get; set; }
		bool oldclock = false;

		List<string> Labels = new List<string>{ };
		List<byte> LabelPoints = new List<byte> { };

		protected override void CircuitLogicUpdate()
		{
			if (code == null)
			{
				code = new string[]{""};
				if (File.Exists(Directory.GetCurrentDirectory() + codeloc))
				{
					code = File.ReadAllLines(Directory.GetCurrentDirectory() + codeloc);
				}
			}
			if(Registers == null)
			{
				Registers = new byte[]{0, 0, 0};
			}
			if (Memory == null)
			{
				Memory = new byte[16];
			}
			//IGConsole.Log(Registers[0]);
			if (this.Inputs[0].On)
			{
				//code = LogiProcessers.CodeGlobal;
				if (File.Exists(Directory.GetCurrentDirectory() + codeloc))
				{
					code = File.ReadAllLines(Directory.GetCurrentDirectory() + codeloc);
				}else
				{
					IGConsole.Log($"{Directory.GetCurrentDirectory() + codeloc} is not a real file. REMEMBER: it's looking for that location in the TUNG directory!!!");
				}
				Line = 0;
				Registers[0] = 0;
				Registers[1] = 0;
				Registers[2] = 0;
				foreach(CircuitOutput c in this.Outputs)
				{
					c.On = false;
				}
			}
			if (this.Inputs[1].On)
			{
				codeloc = LogiProcessers.CodeLocationGlobal;
				if (File.Exists(Directory.GetCurrentDirectory() + codeloc))
				{
					code = File.ReadAllLines(Directory.GetCurrentDirectory() + codeloc);
				}
				else
				{
					IGConsole.Log($"{Directory.GetCurrentDirectory() + codeloc} is not a real file. REMEMBER: it's looking for that location in the TUNG directory!!!");
				}
				Line = 0;
				Registers[0] = 0;
				Registers[1] = 0;
				foreach (CircuitOutput c in this.Outputs)
				{
					c.On = false;
				}
			}
			if (this.Inputs[2].On != oldclock)
			{
				oldclock = this.Inputs[2].On;

				string loc = code[Line].ToLower();
				string[] ops = loc.Split(' ');
				if (ops.Length > 1)
				{
					if (ops[0] == "-")
					{
						Labels.Add(ops[1]);
						LabelPoints.Add(Line);
					}
					if (ops[0] == "debug")
					{
						IGConsole.Log($"A Register: {Registers[0]}");
						IGConsole.Log($"B Register: {Registers[1]}");
						IGConsole.Log($"Flags Register: {Registers[2]}");
						IGConsole.Log($"Program Counter: {Line}");
						int i = 0;
						foreach (byte M in Memory)
						{
							IGConsole.Log($"Memory address {i}: {M}");
							i++;
						}
					}
					if (ops[0] == "lda")
					{
						if (ops[1] == "b")
						{
							Registers[0] = Registers[1];
						}
						else if (ops[1] == "in")
						{
							byte value = 0;
							int position = 1;
							byte slot = 0;
							if (ops.Length > 2)
							{
								try
								{
									slot = Convert.ToByte(ops[2]);
								}
								catch (System.FormatException)
								{
									IGConsole.Log("Tried to set slot to something that's not a number or out of range(0 - 255)");
								}
							}
							for (int i = 3 + (slot * 8); i < this.Inputs.Length & i < 11 + (slot * 8); i++)
							{
								if (this.Inputs[i].On)
								{
									value = (byte)(value + position);
								}
								position = position * 2;
							}
							Registers[0] = value;
						}
						else if (ops[1] == "flags")
						{
							Registers[0] = Registers[2];
						}
						else if (ops[1] == "pc")
						{
							Registers[0] = Line;
						}
						else if (ops[1] == "mem")
						{
							if (ops.Length > 2)
							{
								try
								{
									byte value = Convert.ToByte(ops[2]);
									if (value < 16)
									{
										Registers[0] = Memory[value];
									}
									else
									{
										Registers[0] = 0;
										IGConsole.Log("Tried to set register A to a memory address thats out of range(0 - 15), so A was set to 0!");
									}
								}
								catch (System.FormatException)
								{
									Registers[0] = 0;
									IGConsole.Log("Tried to set register A to a memory address thats out of range(0 - 15), so A was set to 0!");
								}
							}
							else
							{
								IGConsole.Log("To few perameters");
							}
						}
						else
						{
							try
							{
								byte value = Convert.ToByte(ops[1]);
								Registers[0] = value;
							}
							catch (System.FormatException)
							{
								Registers[0] = 0;
								IGConsole.Log("Tried to set register A to something that's not a number, register, input, or out of range(0 - 255), so A was set to 0!");
							}
						}
					}
					if (ops[0] == "sta")
					{
						if (ops[1] == "b")
						{
							Registers[1] = Registers[0];
						}
						else if (ops[1] == "out")
						{
							byte slot = 0;
							if (ops.Length > 2)
							{
								try
								{
									slot = Convert.ToByte(ops[2]);
								}
								catch (System.FormatException)
								{
									IGConsole.Log("Tried to set slot to something that's not a number or out of range(0 - 255)");
								}
							}
							for (int i = slot * 8; i < this.Outputs.Length & i < 8 + (slot * 8); i++)
							{
								this.Outputs[i].On = (Registers[0] & (1 << ((i - (slot * 8)) + 1) - 1)) != 0;
							}
						}
						else if (ops[1] == "mem")
						{
							if (ops.Length > 2)
							{
								try
								{
									byte value = Convert.ToByte(ops[2]);
									if (value < 16)
									{
										Memory[value] = Registers[0];
									}
									else
									{
										IGConsole.Log("Tried to store Register A to a memory address thats out of range(0 - 15), so nothing happened!");
									}
								}
								catch (System.FormatException)
								{
									IGConsole.Log("Tried to store Register A to a memory address thats out of range(0 - 15), so nothing happened!");
								}
							}
							else
							{
								IGConsole.Log("To few perameters");
							}
						}
						else
						{
							IGConsole.Log("That register or output doesn't exist!");
						}
					}
					if (ops[0] == "ldb")
					{
						if (ops[1] == "a")
						{
							Registers[1] = Registers[0];
						}
						else if (ops[1] == "in")
						{
							byte value = 0;
							int position = 1;
							byte slot = 0;
							if (ops.Length > 2)
							{
								try
								{
									slot = Convert.ToByte(ops[2]);
								}
								catch (System.FormatException)
								{
									IGConsole.Log("Tried to set slot to something that's not a number or out of range(0 - 255)");
								}
							}
							for (int i = 3 + (slot * 8); i < this.Inputs.Length & i < 11 + (slot * 8); i++)
							{
								value = (byte)(value + Convert.ToInt32(this.Inputs[i].On) * position);
								position = position * 2;
							}
							Registers[1] = value;
						}
						else if (ops[1] == "flags")
						{
							Registers[1] = Registers[2];
						}
						else if (ops[1] == "pc")
						{
							Registers[1] = Line;
						}
						else if (ops[1] == "mem")
						{
							if (ops.Length > 2)
							{
								try
								{
									byte value = Convert.ToByte(ops[2]);
									if (value < 16)
									{
										Registers[1] = Memory[value];
									}
									else
									{
										Registers[1] = 0;
										IGConsole.Log("Tried to set register B to a memory address thats out of range(0 - 15), so A was set to 0!");
									}
								}
								catch (System.FormatException)
								{
									Registers[1] = 0;
									IGConsole.Log("Tried to set register B to a memory address thats out of range(0 - 15), so A was set to 0!");
								}
							}
							else
							{
								IGConsole.Log("To few perameters");
							}
						}
						else
						{
							try
							{
								byte value = Convert.ToByte(ops[1]);
								Registers[1] = value;
							}
							catch (System.FormatException)
							{
								Registers[1] = 0;
								IGConsole.Log("Tried to set register B to something that's not a number, register, input, or out of range(0 - 255), so B was set to 0!");
							}
						}
					}
					if (ops[0] == "stb")
					{
						if (ops[1] == "a")
						{
							Registers[0] = Registers[1];
						}
						else if (ops[1] == "out")
						{
							byte slot = 0;

							if (ops.Length > 2)
							{
								try
								{
									slot = Convert.ToByte(ops[2]);
								}
								catch (System.FormatException)
								{
									IGConsole.Log("Tried to set slot to something that's not a number or out of range(0 - 255)");
								}
							}
							for (int i = slot * 8; i < this.Outputs.Length & i < 8 + (slot * 8); i++)
							{
								this.Outputs[i].On = (Registers[1] & (1 << ((i - (slot * 8)) + 1) - 1)) != 0;
							}
						}
						else if (ops[1] == "mem")
						{
							if (ops.Length > 2)
							{
								try
								{
									byte value = Convert.ToByte(ops[2]);
									if (value < 16)
									{
										Memory[value] = Registers[1];
									}
									else
									{
										IGConsole.Log("Tried to store Register B to a memory address thats out of range(0 - 15), so nothing happened!");
									}
								}
								catch (System.FormatException)
								{
									IGConsole.Log("Tried to store Register B to a memory address thats out of range(0 - 15), so nothing happened!");
								}
							}
							else
							{
								IGConsole.Log("To few perameters");
							}
						}
						else
						{
							IGConsole.Log("That register or output doesn't exist!");
						}
					}
					string[] ALUBinaryOps = { "add", "sub", "adc", "suc", "or", "nor", "and", "nand", "xor", "xnor" };
					string[] ALUUnaryOps = { "shl", "shr", "rol", "ror" };
					bool dontResetCarry = false;
					int ALU(string op, byte a, byte b)
					{
						int Out = 0;
						if (op == "add")
						{
							Out = a + b;
						}
						if (op == "sub")
						{
							Out = a - b;
						}
						if (op == "adc")
						{
							Out = a + b + (Registers[2] % 2);
							dontResetCarry = true;
						}
						if (op == "suc")
						{
							Out = a - (b - 1 + (Registers[2] % 2));
							dontResetCarry = true;
						}
						if (op == "or")
						{
							Out = a | b;
						}
						if (op == "nor")
						{
							Out = ~(a | b);
						}
						if (op == "and")
						{
							Out = a & b;
						}
						if (op == "nand")
						{
							Out = ~(a & b);
						}
						if (op == "xor")
						{
							Out = a ^ b;
						}
						if (op == "xnor")
						{
							Out = ~(a ^ b);
						}
						if (op == "shl")
						{
							Out = a << 1;
						}
						if (op == "shr")
						{
							Out = a >> 1;
						}
						if (op == "rol")
						{
							Out = (a << 1) + (Registers[2] % 2);
							if (a > 127 && Registers[2] % 2 == 0)
							{
								Registers[2]++;
							}
							dontResetCarry = true;
						}
						if (op == "ror")
						{
							Out = (a >> 1) + (Registers[2] % 2) * 128;
							if (a % 1 == 1 && Registers[2] % 2 == 0)
							{
								Registers[2]++;
							}
							dontResetCarry = true;
						}
						return Out;
					}
					foreach (string aluop in ALUBinaryOps)
					{
						if (ops[0] == aluop)
						{
							if (ops.Length > 2)
							{
								byte TTA = 0;
								if (ops[2] == "a")
								{

									if (ops[1] == "b")
									{
										TTA = Registers[1];
									}
									else
									{
										byte num = 0;
										try
										{
											num = Convert.ToByte(ops[1]);
										}
										catch (System.FormatException)
										{
											IGConsole.Log("Tried to add something to register A something that's not a number, register or out of range(0 - 255)");
										}
										TTA = num;
									}
									int ia = ALU(aluop, Registers[0], TTA);
									byte oldv = Registers[0];
									Registers[0] = (byte)ALU(aluop, Registers[0], TTA);
									Registers[2] = 0;
									if (dontResetCarry)
									{
										Registers[2]++;
									}
									if (ia > 255 && !dontResetCarry)
									{
										Registers[2] += 1;
									}
									if (ia == 0)
									{
										Registers[2] += 2;
									}
									if ((oldv > 127 && TTA > 127 && Registers[0] <= 127) || (oldv <= 127 && TTA <= 127 && Registers[0] > 127))
									{
										Registers[2] += 4;
									}
									if (Registers[0] == 0)
									{
										Registers[2] += 8;
									}
									if (Registers[0] > 127)
									{
										Registers[2] += 16;
									}

								}
								else if (ops[2] == "b")
								{
									if (ops[1] == "a")
									{
										TTA = Registers[0];
									}
									else
									{
										byte num = 0;
										try
										{
											num = Convert.ToByte(ops[1]);
										}
										catch (System.FormatException)
										{
											IGConsole.Log("Tried to add something to register B something that's not a number, register or out of range(0 - 255)");
										}
										TTA = num;
									}
									int ia = ALU(aluop, Registers[1], TTA);
									byte oldv = Registers[1];
									Registers[1] = (byte)ALU(aluop, Registers[1], TTA);
									Registers[2] = 0;
									if (dontResetCarry)
									{
										Registers[2]++;
									}
									if (ia > 255 && !dontResetCarry)
									{
										Registers[2] += 1;
									}
									if (ia == 0)
									{
										Registers[2] += 2;
									}
									if ((oldv > 127 && TTA > 127 && Registers[1] <= 127) || (oldv <= 127 && TTA <= 127 && Registers[1] > 127))
									{
										Registers[2] += 4;
									}
									if (Registers[1] == 0)
									{
										Registers[2] += 8;
									}
									if (Registers[1] > 127)
									{
										Registers[2] += 16;
									}
								}
								else
								{
									IGConsole.Log("That register doesn't exist!");
								}
							}
							else
							{
								IGConsole.Log("Not enough arguments!");
							}
						}
					}
					foreach (string aluop in ALUUnaryOps)
					{
						if (ops[0] == aluop)
						{
							if (ops.Length > 1)
							{
								byte TTA = 0;
								if (ops[1] == "a")
								{
									int ia = ALU(aluop, Registers[0], TTA);
									byte oldv = Registers[0];
									Registers[0] = (byte)ALU(aluop, Registers[0], TTA);
									Registers[2] = 0;
									if (dontResetCarry)
									{
										Registers[2]++;
									}
									if (ia > 255 && !dontResetCarry)
									{
										Registers[2] += 1;
									}
									if (ia == 0)
									{
										Registers[2] += 2;
									}
									if ((oldv > 127 && TTA > 127 && Registers[0] <= 127) || (oldv <= 127 && TTA <= 127 && Registers[0] > 127))
									{
										Registers[2] += 4;
									}
									if (Registers[0] == 0)
									{
										Registers[2] += 8;
									}
									if (Registers[0] > 127)
									{
										Registers[2] += 16;
									}

								}
								else if (ops[1] == "b")
								{
									int ia = ALU(aluop, Registers[1], TTA);
									byte oldv = Registers[1];
									Registers[1] = (byte)ALU(aluop, Registers[1], TTA);
									Registers[2] = 0;
									if (dontResetCarry)
									{
										Registers[2]++;
									}
									if (ia > 255 && !dontResetCarry)
									{
										Registers[2] += 1;
									}
									if (ia == 0)
									{
										Registers[2] += 2;
									}
									if ((oldv > 127 && TTA > 127 && Registers[1] <= 127) || (oldv <= 127 && TTA <= 127 && Registers[1] > 127))
									{
										Registers[2] += 4;
									}
									if (Registers[1] == 0)
									{
										Registers[2] += 8;
									}
									if (Registers[1] > 127)
									{
										Registers[2] += 16;
									}
								}
								else
								{
									IGConsole.Log("That register doesn't exist!");
								}
							}
							else
							{
								IGConsole.Log("Not enough arguments!");
							}
						}
					}
					bool jumped = false;
					if ((ops[0] == "jmp") || (ops[0] == "jmc" && (Registers[2] & (1 << ((0) + 1) - 1)) == 1) || (ops[0] == "jmz" && (Registers[2] & (1 << ((1) + 1) - 1)) == 1) || (ops[0] == "jmo" && (Registers[2] & (1 << ((2) + 1) - 1)) == 1) || (ops[0] == "jms" && (Registers[2] & (1 << ((3) + 1) - 1)) == 1) || (ops[0] == "jmn" && (Registers[2] & (1 << ((4) + 1) - 1)) == 1))
					{
						int i = 0;
						foreach (string label in Labels)
						{
							if (ops[1] == label)
							{
								Line = (byte)(LabelPoints[i] + 1);
								jumped = true;
							}
							i++;
						}
						if (!jumped)
						{
							if (ops[1] == "a")
							{
								Line = Registers[0];
								jumped = true;
							}
							else if (ops[1] == "b")
							{
								Line = Registers[1];
								jumped = true;
							}
							else
							{
								try
								{
									byte value = Convert.ToByte(ops[1]);
									if (value < code.Length)
									{
										Line = value;
										jumped = true;
									}
									else
									{
										IGConsole.Log($"You tried to jump to a line that doesn't exist! there are only {code.Length} lines!");
									}
								}
								catch (System.FormatException)
								{
									IGConsole.Log("The value for jumping is out of range (0 - 255)!");
								}
							}
						}
					}
					if (!jumped)
					{
						Line++;
						if (Line >= code.Length)
						{
							Line--;
						}
					}
				}
				else
				{
					Line++;
					if (Line >= code.Length)
					{
						Line--;
					}
				}
			}
		}
	}
}
