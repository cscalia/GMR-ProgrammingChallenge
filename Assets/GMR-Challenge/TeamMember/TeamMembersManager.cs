using System;
using System.Collections.Generic;
using System.IO;
using Code.Table;
using Newtonsoft.Json;
using UnityEngine;

public class TeamMembersManager : MonoBehaviour
{
    [SerializeField]
    private UITable UITable;

    private readonly string path = Application.streamingAssetsPath + "/JsonChallenge.json";
    private readonly string diretory = Application.streamingAssetsPath + "/";

    private void Awake()
    {
        LoadJson();
        FolderWatcher(diretory);
    }

    private void LoadJson()
    {
        IJsonLoader jsonLoader = new JsonLoader();
        jsonLoader.LoadJson(path, OnJsonLoaded);
    }

    private void OnJsonLoaded(string json)
    {
        if (!string.IsNullOrEmpty(json))
        {
            UITable.Clear();
            TeamMembersTableModel teamMembersTableModel = JsonConvert.DeserializeObject<TeamMembersTableModel>(json);
            FillTable(teamMembersTableModel);
        } 
    }

    private void FillTable(TeamMembersTableModel teamMembersTableModel)
    {
        UITable.AddTitle(teamMembersTableModel.Title);
        UITable.AddHeaders(teamMembersTableModel.ColumnHeaders);
        List<string> content = new List<string>();
        foreach (var dic in teamMembersTableModel.Data)
        {
            foreach (var column in teamMembersTableModel.ColumnHeaders)
            {
                string text = String.Empty;
                if(dic.TryGetValue(column, out object value))
                {
                    text = value.ToString();
                }
                content.Add(text);
            }
            UITable.AddContent(content.ToArray());
            content.Clear();
        }
    }


    private void FolderWatcher(string watchedFolderPath)
    {
        FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = watchedFolderPath;
        watcher.Filter = "JsonChallenge.json";
        watcher.Changed += OnFileChanged;
        watcher.EnableRaisingEvents = true;
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        UnityMainThreadDispatcher.Instance.Enqueue(LoadJson);
    }


}