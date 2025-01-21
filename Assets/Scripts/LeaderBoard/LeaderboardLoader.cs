using Agava.YandexGames;
using UnityEngine;
using WalletAndScore;

namespace LeaderBoard
{
    public class LeaderboardLoader : MonoBehaviour
    {
        private const int MaxRecordsToShow = 10;
        private const string AnonymousEn = "Anonymous";
        private const string AnonymousRu = "Аноним";
        private const string AnonymousTr = "Anonim";
        private const string EnglishCode = "en";
        private const string RussianCode = "ru";
        private const string TurkishCode = "tr";
        private const string LeaderboardNameText = "IDLeaderboard";

        [SerializeField] private Record[] _records;
        [SerializeField] private PlayerRecord _playerRecord;
        
        private Score _score;

        private void OnDestroy()
        {
            Score.ObjectLoaded -= OnHandleScoreLoaded;
        }

        private void Start()
        {
            DisableAllRecords();
            DontDestroyOnLoad(gameObject);
            Score.ObjectLoaded += OnHandleScoreLoaded;

#if UNITY_WEBGL && !UNITY_EDITOR
        LoadYandexLeaderboard();
#endif
        }

        public void TryRunToRegisterNewMaxScore()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            TryRegisterNewMaxScore();
        }
#endif
        }

        private void OnHandleScoreLoaded(Score score)
        {
            _score = score;
        }

        private void TryRegisterNewMaxScore()
        {
            Leaderboard.GetPlayerEntry(LeaderboardNameText,
                result =>
                {
                    if (_score.ScorePoints > result.score)
                    {
                        Leaderboard.SetScore(LeaderboardNameText, _score.ScorePoints);
                    }
                });
        }

        private void DisableAllRecords()
        {
            _playerRecord.gameObject.SetActive(false);

            foreach (var record in _records)
            {
                record.gameObject.SetActive(false);
            }
        }

        private void LoadYandexLeaderboard()
        {
            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();

                Leaderboard.GetEntries(LeaderboardNameText, (result) =>
                {
                    int recordsToShow =
                        result.entries.Length <= MaxRecordsToShow ? result.entries.Length : MaxRecordsToShow;

                    for (int i = 0; i < recordsToShow; i++)
                    {
                        string name = result.entries[i].player.publicName;

                        if (string.IsNullOrEmpty(name))
                        {
                            string locale = YandexGamesSdk.Environment.i18n.lang;

                            switch (locale)
                            {
                                case EnglishCode:
                                    name = AnonymousEn;
                                    break;

                                case RussianCode:
                                    name = AnonymousRu;
                                    break;

                                case TurkishCode:
                                    name = AnonymousTr;
                                    break;
                            }
                        }

                        _records[i].SetName(name);
                        _records[i].SetScore(result.entries[i].formattedScore);
                        _records[i].SetRank(result.entries[i].rank);
                        _records[i].gameObject.SetActive(true);
                    }
                });

                LoadPlayerScore();
            }
        }

        private void LoadPlayerScore()
        {
            if (YandexGamesSdk.IsInitialized)
            {
                Leaderboard.GetPlayerEntry(LeaderboardNameText, OnSuccessCallback);
            }
        }

        private void OnSuccessCallback(LeaderboardEntryResponse result)
        {
            if (result != null)
            {
                _playerRecord.gameObject.SetActive(true);
                _playerRecord.SetName(result.player.publicName);
                _playerRecord.SetScore(result.score.ToString());
                _playerRecord.SetRank(result.rank);
            }
            else
            {
                _playerRecord.gameObject.SetActive(false);
            }
        }
    }
}