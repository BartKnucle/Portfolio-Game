using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CrazyGoat.Colors
{
  [CreateAssetMenu(menuName = "CrazyGoat/Colors/List")]
  public class ColorList : ScriptableObject
  {
      public List<Color> Value;
  }
}