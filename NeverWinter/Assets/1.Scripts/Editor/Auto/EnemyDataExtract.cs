using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(EnemyDataExtract))]
public class EnemyDataExtractEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Datas"))
        {
            if (target is EnemyDataExtract ede)
            {
                for (int i = 0; i < ede.enemyCtrls.Length; i++)
                {
                    var enemy = ede.enemyCtrls[i];
                    var data = CreateInstance<EnemyData>();
                    data.name = enemy.name;
                    data.Hp = enemy.Max_Hp;
                    data.Atk = enemy.atk;
                    data.Speed = enemy.Enemy_move_Speed;
                    data.Distance = enemy.distance;
                    data.Reward = enemy.Reward;

                    enemy.data = data;
                    AssetDatabase.CreateAsset(data, $"Assets\\1.Scripts\\EnemyDatas\\{data.name}.asset");
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
#endif

[CreateAssetMenu(menuName = "Automation/EnemyDataExtract")]
public class EnemyDataExtract : ScriptableObject
{
    public EnemyCtrl[] enemyCtrls;
}
