using UnityEngine; 
using UnityEditor;

class MeshPostprocessor : AssetPostprocessor {

	void OnPreprocessModel () {
		//Currently broken
		//(assetImporter as ModelImporter).globalScale = 1f; 
	}
} 