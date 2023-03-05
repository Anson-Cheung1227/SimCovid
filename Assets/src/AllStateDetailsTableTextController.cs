using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllStateDetailsTableTextController : MonoBehaviour
{
    public int Id;
    [SerializeField] private TextMeshProUGUI _stateNameText;
    [SerializeField] private TextMeshProUGUI _stateInfectionsText;
    [SerializeField] private TextMeshProUGUI _stateInHospitalText;
    [SerializeField] private TextMeshProUGUI _stateRecoveredText;
    [SerializeField] private TextMeshProUGUI _stateDeceasedText;
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.Instance.OnUpdateAllStateDetailsTable += OnUpdateStateDetailsTable;
    }
    private void OnUpdateStateDetailsTable(List<State> stateList)
    {
        _stateNameText.text = stateList[Id].Name;
        _stateInfectionsText.text = UIManager.LongToString(stateList[Id].InfectionsLong);
        _stateInHospitalText.text = UIManager.LongToString(stateList[Id].InHospitalLong);
        _stateRecoveredText.text = UIManager.LongToString(stateList[Id].RecoveredLong);
        _stateDeceasedText.text = UIManager.LongToString(stateList[Id].DeceasedLong);
    }
}
