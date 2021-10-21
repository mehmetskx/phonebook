using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PhoneBook.Models
{
    [Serializable]
    public class ResponseModel<T>
    {
        private bool _isSuccess;
        private List<Error> _ErrorList = null;

        public ResponseModel()
        {
            _isSuccess = false;
            ErrorList = new List<Error>();
        }

        public T Data { get; set; }

        public int TotalRowCount { get; set; }

        public bool IsSuccess
        {
            get
            {
                if (ErrorList != null && ErrorList.Count > 0)
                {
                    _isSuccess = false;
                }
                else _isSuccess = true;

                return _isSuccess;
            }
            set
            {
                _isSuccess = value;
            }
        }


        [XmlArray("ErrorList")]
        [XmlArrayItem("Error")]
        public List<Error> ErrorList
        {
            get
            {
                return _ErrorList;
            }

            set
            {
                _ErrorList = value;
            }
        }

        public object First()
        {
            throw new NotImplementedException();
        }
    }
}
