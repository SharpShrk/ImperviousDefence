using TMPro;
using UnityEngine;

namespace LeaderBoard
{
    public class Record : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _rank;

        public void SetName(string name)
        {
            _name.text = name;
        }

        public void SetScore(string score)
        {
            _score.text = score;
        }

        public void SetRank(int rank)
        {
            _rank.text = rank.ToString();
        }
    }
}