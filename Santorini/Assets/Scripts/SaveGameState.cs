using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameState : MonoBehaviour
{
    SantoriniData _santoriniData = new SantoriniData();

    public void SaveState(List<Player> players, Player activePlayer, Board board)
    {
        List<PlayerData> playerDatas = new List<PlayerData>();
        List<TileData> tileDatas = new List<TileData>();

        foreach (Player player in players)
        {
            PlayerData playerData = new PlayerData();
            playerData.activePlayer = player == activePlayer;
            List<WorkerData> workerDatas = new List<WorkerData>();

            foreach (Worker worker in player.GetWorkers())
            {
                WorkerData workerData = new WorkerData();

                TileData tileData = new TileData();
                tileData.workerOnTile = worker.GetTile().HasWorkerOnTile();
                tileData.towerPieces = worker.GetTile().GetTowerPieceCount();
                tileData.domed = worker.GetTile().IsDomed();

                workerData.tile = tileData;
                workerData.colour = (int) worker.GetColour();
                workerData.gender = (int) worker.GetGender();
                workerDatas.Add(workerData);

                if(worker == player.GetSelectedWorker())
                {
                    playerData.selectedWorker = workerData;
                }
            }

            playerDatas.Add(playerData);
            playerData.workers = workerDatas;
        }

        foreach(Tile tile in board.GetTiles())
        {
            TileData tileData = new TileData();
            tileData.workerOnTile = tile.HasWorkerOnTile();
            tileData.towerPieces = tile.GetTowerPieceCount();
            tileData.domed = tile.IsDomed();

            tileDatas.Add(tileData);
        }

        _santoriniData.players = playerDatas;
        _santoriniData.tiles = tileDatas;

        SaveToJson();
    }

    void SaveToJson()
    {
        string santorini = JsonUtility.ToJson(_santoriniData);
        System.IO.File.WriteAllText("../SantoriniData.json", santorini);
    }
}

[System.Serializable]
public class SantoriniData
{
    public List<PlayerData> players = new List<PlayerData>();
    public List<TileData> tiles = new List<TileData>();
}

[System.Serializable]
public class PlayerData
{
    public bool activePlayer;
    public List<WorkerData> workers = new List<WorkerData>();
    public WorkerData selectedWorker;
}

[System.Serializable]
public class WorkerData
{
    public TileData tile;
    public int colour;
    public int gender;
}

[System.Serializable]
public class TileData
{
    public bool workerOnTile;
    public int towerPieces;
    public bool domed;
}
