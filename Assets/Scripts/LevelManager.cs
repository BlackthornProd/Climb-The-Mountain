using UnityEngine;

public class LevelManager : MonoBehaviour {


	public GameObject[] rooms;
	public Vector2 numberRooms;

	[Space]

	public GameObject[] specialTiles;
	public Vector2 numberOfSpecialTilesPerRoomMinMax;

		
	void Start () {
		for (int y = 0; y < numberRooms.y; y++) {
			for (int x = 0; x < numberRooms.x; x++) {
				SpawnRoom (y, x);
			}
		}
	}
		
	void SpawnRoom (int y, int x) {
		GameObject newRoom = (GameObject)Instantiate (rooms[Random.Range (0, rooms.Length)], new Vector2 (x * 10, y * 10), Quaternion.identity);
		newRoom.name = "Room" + y + "/" + x;
		int numberOfSpecialTilesInRoom = (int)Random.Range (numberOfSpecialTilesPerRoomMinMax.x, numberOfSpecialTilesPerRoomMinMax.y + 1);
		for (int i = 0; i < numberOfSpecialTilesInRoom; i++) {
			Transform platforms = newRoom.transform.Find ("Platforms");
			int numberOfTilesInRoom = platforms.childCount;
			int randomTileInRoomIndex = Random.Range (0, numberOfTilesInRoom);
			Transform tileToChangeTransform = platforms.GetChild (randomTileInRoomIndex);
			GameObject tileToChangeGO = tileToChangeTransform.gameObject;
			Destroy (tileToChangeGO);
			GameObject newSpecialTile = (GameObject)Instantiate (specialTiles[Random.Range (0, specialTiles.Length)], tileToChangeTransform.position, Quaternion.identity);
			newSpecialTile.transform.SetParent (newRoom.transform);
		}

	}

}
