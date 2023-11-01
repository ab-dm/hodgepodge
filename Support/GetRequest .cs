using System.Net;

namespace Support
{
    // Получить запрос
    public class GetRequest
    {
        HttpWebRequest _request; // объек запроса
        string _address;

        public Dictionary<string, string> Headers { get; set; } // Для передачи заголовков

        public string Response { get; set; }

        public string Accept { get; set; }
        public string Host { get; set; }
        public WebProxy Proxy { get; set; }

        public GetRequest(string address)
        {
            _address = address;
            Headers = new Dictionary<string, string>();
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

        public void Run(CookieContainer cookieContainer) // Запускает созданный запрос
        {
            // создаём запрос, привести к HttpWebRequest
            _request = (HttpWebRequest)WebRequest.Create(_address);
            // указать, что данный запрос является Get запросом
            _request.Method = "GET";
            // перед выполнением запроса, cookieContainer запроса который мы передали в момент запроса
            _request.CookieContainer = cookieContainer;
            // адрес фидлера в запрос
            _request.Proxy = Proxy;
            _request.Accept = Accept;
            _request.Host = Host;


            foreach (var pair in Headers)
            {
                _request.Headers.Add(pair.Key, pair.Value);
            }

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