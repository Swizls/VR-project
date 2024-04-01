using TMPro;
using UnityEngine;

namespace UI
{
    public class ResourcePresenterElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _amountText;

        void Start()
        {
            if (_nameText == null || _amountText == null)
                throw new System.ArgumentNullException("Text fields are null");
        }

        public void SetValues(string name, int amount)
        {
            _nameText.text = name;
            _amountText.text = amount.ToString();
        }
    }

}
