using System.Net;

namespace Support
{
    // Получить запрос
    public class GetRequest
    {
        HttpWebRequest _request; // объек запроса
        string _address;

        public string Response {  get; set; }

        public GetRequest(string address)
        {
            _address = address;
        }

        public void Run() // Запускает созданный запрос
        {
            // создаём запрос, привести к HttpWebRequest
            _request = (HttpWebRequest)WebRequest.Create(_address);
            // указать, что данный запрос является Get запросом
            _request.Method = "GET";

            try
            {
                // Создадим объект ответа ВебСервера
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                var stream = response.GetResponseStream();

                if (stream != null)
                {
                    // записываем ответ от ВебСервера
                    Response = new StreamReader(stream).ReadToEnd();
                }
            }
            catch (Exception)
            {
            }
        } 
    }
}