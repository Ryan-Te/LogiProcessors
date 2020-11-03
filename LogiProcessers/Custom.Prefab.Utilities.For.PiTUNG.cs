using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static UnityEngine.Random;

class CPUFP
{
	//The creatCube Function Is A Modified Version of a Function That Falsepattern Made
	//The Other 2 Functions Were Made By Me
	//Please Give Credit Where Credit is due
	const float baseSize = 0.3f;
	internal static GameObject createCube(int X, int Z)
		{
			//Create the container game object, and activate it
			var obj = new GameObject();
			obj.SetActive(true);
			//Create the cube itself, bind it's position to the root object, and scale to the board grid size
			var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.parent = obj.transform;
			float rZ = 0;
			float rX = 0;
			if (((float)Z / 2) == Math.Round(((float)Z / 2)))
			{
				rZ = 0.5f;
			}
			if (((float)X / 2) == Math.Round(((float)X / 2)))
			{
				rX = 0.5f;
			}



			cube.transform.localScale = new Vector3(baseSize * Z, baseSize, baseSize * X);
			cube.transform.localPosition = new Vector3(rZ * baseSize, baseSize / 2, rX * baseSize);
			var cubeRenderer = cube.GetComponent<Renderer>();
			cubeRenderer.material.color = new Color(1, 1, 1);
			return obj;
		}
	internal static GameObject createCube(int X, int Y, int Z)
		{
			
			//Create the container game object, and activate it
			var obj = new GameObject();
			obj.SetActive(true);
			//Create the cube itself, bind it's position to the root object, and scale to the board grid size
			var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.parent = obj.transform;
			float rZ = 0;
			float rX = 0;
			if (((float)Z / 2) == Math.Round(((float)Z / 2)))
			{
				rZ = 0.5f;
			}
			if (((float)X / 2) == Math.Round(((float)X / 2)))
			{
				rX = 0.5f;
			}



			cube.transform.localScale = new Vector3(baseSize * Z, baseSize * Y, baseSize * X);
			cube.transform.localPosition = new Vector3(rZ * baseSize, baseSize * Y / 2, rX * baseSize);
			var cubeRenderer = cube.GetComponent<Renderer>();
			cubeRenderer.material.color = new Color(1, 1, 1);
			return obj;
		}

	internal static GameObject createCube(int X, int Z,Color C)
	{
		//Create the container game object, and activate it
		var obj = new GameObject();
		obj.SetActive(true);
		//Create the cube itself, bind it's position to the root object, and scale to the board grid size
		var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.parent = obj.transform;
		float rZ = 0;
		float rX = 0;
		if (((float)Z / 2) == Math.Round(((float)Z / 2)))
		{
			rZ = 0.5f;
		}
		if (((float)X / 2) == Math.Round(((float)X / 2)))
		{
			rX = 0.5f;
		}



		cube.transform.localScale = new Vector3(baseSize * Z, baseSize, baseSize * X);
		cube.transform.localPosition = new Vector3(rZ * baseSize, baseSize / 2, rX * baseSize);
		var cubeRenderer = cube.GetComponent<Renderer>();
		cubeRenderer.material.color = C;
		return obj;
	}
	internal static GameObject createCube(int X, int Y, int Z, Color C)
	{

		//Create the container game object, and activate it
		var obj = new GameObject();
		obj.SetActive(true);
		//Create the cube itself, bind it's position to the root object, and scale to the board grid size
		var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.parent = obj.transform;
		float rZ = 0;
		float rX = 0;
		if (((float)Z / 2) == Math.Round(((float)Z / 2)))
		{
			rZ = 0.5f;
		}
		if (((float)X / 2) == Math.Round(((float)X / 2)))
		{
			rX = 0.5f;
		}



		cube.transform.localScale = new Vector3(baseSize * Z, baseSize * Y, baseSize * X);
		cube.transform.localPosition = new Vector3(rZ * baseSize, baseSize * Y / 2, rX * baseSize);
		var cubeRenderer = cube.GetComponent<Renderer>();
		cubeRenderer.material.color = C;
		return obj;
	}
	internal static Vector3 getPegPos(int sizeX, int sizeZ, string Side)
		{
			Vector3 Output;
			if (Side == "F")
			{
				Output = new Vector3(0, 0.15f, -0.15f * sizeX);

			}
			else if (Side == "B")
			{
				Output = new Vector3(0, 0.15f, 0.15f * sizeX);
			}
			else if (Side == "R")
			{
				Output = new Vector3(-0.15f * sizeZ, 0.15f, 0);
			}
			else if (Side == "L")
			{
				Output = new Vector3(0.15f * sizeZ, 0.15f, 0);
			}
			else if (Side == "T")
			{
				Output = new Vector3(0, 0.3f, 0);
			}
			else
			{
				Output = new Vector3(0, 0, 0);
			}
			if (((float)sizeZ / 2) == Math.Round(((float)sizeZ / 2)))
			{
				Output = Output + new Vector3(0.5f * 0.3f, 0, 0);
			}
			if (((float)sizeX / 2) == Math.Round(((float)sizeX / 2)))
			{
				Output = Output + new Vector3(0, 0, 0.5f * 0.3f);
			}
			return Output;
		}
	internal static Vector3 getPegPos(int sizeX, int sizeY, int sizeZ, string Side)
		{
			Vector3 Output;
			if (Side == "F")
			{
				Output = new Vector3(0, 0.15f * sizeY, -0.15f * sizeX);
			}
			else if (Side == "B")
			{
				Output = new Vector3(0, 0.15f * sizeY, 0.15f * sizeX);
			}
			else if (Side == "R")
			{
				Output = new Vector3(-0.15f * sizeZ, 0.15f * sizeY, 0);
			}
			else if (Side == "L")
			{
				Output = new Vector3(0.15f * sizeZ, 0.15f * sizeY, 0);
			}
			else if (Side == "T")
			{
				Output = new Vector3(0, 0.3f * sizeY, 0);
			}
			else
			{
				Output = new Vector3(0, 0, 0);
			}
			if (((float)sizeZ / 2) == Math.Round(((float)sizeZ / 2)))
			{
				Output = Output + new Vector3(0.5f * 0.3f, 0, 0);
			}
			if (((float)sizeX / 2) == Math.Round(((float)sizeX / 2)))
			{
				Output = Output + new Vector3(0, 0, 0.5f * 0.3f);
			}
			return Output;
		}
	internal static Vector3 getPegPos(int sizeX, int sizeZ, string Side, float offsetX, float offsetY)
	{
		Vector3 Output;
		if (Side == "F")
		{
			Output = new Vector3(-0.3f * offsetX, 0.15f + (offsetY * 0.3f), -0.15f * sizeX);

		}
		else if (Side == "B")
		{
			Output = new Vector3(offsetX * 0.3f, 0.15f + (offsetY * -0.3f), 0.15f * sizeX);
		}
		else if (Side == "R")
		{
			Output = new Vector3(-0.15f * sizeZ, 0.15f + (offsetY * 0.3f), (offsetX * 0.3f));
		}
		else if (Side == "L")
		{
			Output = new Vector3(0.15f * sizeZ, 0.15f + (offsetY * -0.3f), (offsetX * -0.3f));
		}
		else if (Side == "T")
		{
			Output = new Vector3((offsetX * 0.3f), 0.3f, (offsetY * -0.3f));
		}
		else
		{
			Output = new Vector3(0, 0, 0);
		}
		if (((float)sizeZ / 2) == Math.Round(((float)sizeZ / 2)))
		{
			Output = Output + new Vector3(0.5f * 0.3f, 0, 0);
		}
		if (((float)sizeX / 2) == Math.Round(((float)sizeX / 2)))
		{
			Output = Output + new Vector3(0, 0, 0.5f * 0.3f);
		}
		return Output;
	}
	internal static Vector3 getPegPos(int sizeX, int sizeY, int sizeZ, string Side, float offsetX, float offsetY)
	{
		Vector3 Output;
		if (Side == "F")
		{
			Output = new Vector3(-0.3f * offsetX, (0.15f * sizeY) + (offsetY * 0.3f), -0.15f * sizeX);

		}
		else if (Side == "B")
		{
			Output = new Vector3(offsetX * 0.3f, (0.15f * sizeY) - (offsetY * -0.3f), 0.15f * sizeX);
		}
		else if (Side == "R")
		{
			Output = new Vector3(-0.15f * sizeZ, (0.15f * sizeY) + (offsetY * 0.3f), (offsetX * 0.3f));
		}
		else if (Side == "L")
		{
			Output = new Vector3(0.15f * sizeZ, (0.15f * sizeY) - (offsetY * -0.3f), (offsetX * -0.3f));
		}
		else if (Side == "T")
		{
			Output = new Vector3((offsetX * 0.3f), (0.3f * sizeY), (offsetY * -0.3f));
		}
		else
		{
			Output = new Vector3(0, 0, 0);
		}
		if (((float)sizeZ / 2) == Math.Round(((float)sizeZ / 2)))
		{
			Output = Output + new Vector3(0.5f * 0.3f, 0, 0);
		}
		if (((float)sizeX / 2) == Math.Round(((float)sizeX / 2)))
		{
			Output = Output + new Vector3(0, 0, 0.5f * 0.3f);
		}
		return Output;
	}
	internal static Quaternion getPegQuat(string Side)
		{
			if (Side == "F")
			{
				return Quaternion.Euler(-90, 0, 0);
			}
			else if (Side == "B")
			{
				return Quaternion.Euler(90, 0, 0);
			}
			else if (Side == "R")
			{
				return Quaternion.Euler(0, 0, 90);
			}
			else if (Side == "L")
			{
				return Quaternion.Euler(0, 0, -90);
			}
			else
			{
				return Quaternion.Euler(0, 0, 0);
			}
		}

}