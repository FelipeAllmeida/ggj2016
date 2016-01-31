using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    #region Const Data

    [SerializeField] public const int Width = 32;
    [SerializeField] public const int Height = 24;
    
    public const int TileCount = Width * Height;

    #endregion

    #region PublicData
    public LevelData[] LevelData;
    public TileData[] TileData;
    List<GameObject> mapTileList;
    public int CurrentLevel { private set; get; }
    public int DebugLevel = 0;
    public float nextMapTime = 0;
    public int itemQuantity = 0;
    #endregion


    void Start()
    {
        mapTileList = new List<GameObject>();
        foreach (TileData tile in TileData)
        {
            tile.gameObject.SetActive(false);
        }
        CurrentLevel = 0;
        GenerateLevel(CurrentLevel);
        AudioController.instance.Initialize();
        AudioController.instance.PlayMain();
    }

    public void NextLevel(int increase = 1)
    {
        itemQuantity = 0;
        CurrentLevel += increase;
        GenerateLevel(CurrentLevel);

    }

    public void ResetLevel()
    {

        TotemController[] __totems = FindObjectsOfType<TotemController>();
        foreach(TotemController __totem in __totems)
        {
            Destroy(__totem.gameObject);
        }
        GameObject[] __keySpawners = GameObject.FindGameObjectsWithTag("KeySpawner");
        foreach (GameObject __spawner in __keySpawners)
        {
            __spawner.GetComponent<Spawner>().isActive = true;
        }
        GameObject[] __keys = GameObject.FindGameObjectsWithTag("Key");
        foreach(GameObject __key in __keys)
        {
            Destroy(__key);
        }
        GameObject[] __itens = GameObject.FindGameObjectsWithTag("RitualItem");
        foreach (GameObject __item in __itens)
        {
            Destroy(__item);
        }
        Door[] __doors = FindObjectsOfType<Door>();
        foreach(Door __door in __doors)
        {
            __door.Reset();
        }
        Spikes[] __spikes = FindObjectsOfType<Spikes>();
        foreach (Spikes __spike in __spikes)
        {
            __spike.Reset();
        }
        GhostController[] __ghosts = FindObjectsOfType<GhostController>();
        foreach (GhostController __ghost in __ghosts)
        {
            Destroy(__ghost.gameObject);
        }
    }

    private void EraseLevel()
    {
        
        foreach (GameObject obj in mapTileList)
        {
            Destroy(obj);
        }
        mapTileList.Clear();
    }

    private void GenerateLevel(int level)
    {
        EraseLevel();
        int __newLevel = level;

        if (__newLevel >= LevelData.Length)
        {
            Application.LoadLevel("results");
            return;
        }
        LevelData __levelData = LevelData[__newLevel];
        Vector3 origin = new Vector3(Width * -0.5f + 0.5f, Height * -0.5f + 0.5f, 0) + transform.position;
        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                Color __color = __levelData.image.GetPixel(x, z);
                foreach (TileData tile in TileData)
                {
                    if (__color == tile.colour)
                    {
                        if (tile.name == "RitualItemTile")
                        {
                            itemQuantity++;
                        }
                        GameObject newTile = Instantiate(tile.gameObject, origin + new Vector3(x * 2, 0, z * 2), Quaternion.identity) as GameObject;
                        Object.Destroy(newTile.GetComponent<TileData>());
                        newTile.transform.parent = transform;
                        newTile.name = tile.name + " (" + (x + (z * Width)) + ")";
                        newTile.SetActive(true);
                        mapTileList.Add(newTile);
                    }
                }
            }
        }

    }

    void Update()
    {

        if (nextMapTime > 0 && Time.time > nextMapTime)
        {
            Explosion[] es = FindObjectsOfType<Explosion>();
            foreach (Explosion e in es)
            {
                Destroy(e.gameObject);
            }

            Victory victory = FindObjectOfType<Victory>();
            if (victory)
            {
                Destroy(victory.gameObject);
            }
            GenerateLevel(CurrentLevel);
            nextMapTime = 0;
        }

        if (!Application.isEditor)
            return;

        if (Input.GetKeyDown(KeyCode.F2))
        {
            CurrentLevel = DebugLevel;
            GenerateLevel(CurrentLevel);
        }

        if (Input.GetKeyDown(KeyCode.F5))
            GenerateLevel(CurrentLevel);
    }
}