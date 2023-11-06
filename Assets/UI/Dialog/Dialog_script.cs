using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class Dialog_script : ScriptableObject
{
	public List<DialogDB> Earth; // Replace 'EntityType' to an actual type that is serializable.
	public List<DialogDB> Dino; // Replace 'EntityType' to an actual type that is serializable.
	public List<DialogDB> Desert; // Replace 'EntityType' to an actual type that is serializable.
	public List<DialogDB> Earth_Ending; // Replace 'EntityType' to an actual type that is serializable.

}
