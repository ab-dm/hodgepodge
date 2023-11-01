using System.Net;
using System.Text;

namespace Support
{
    // Отправляем данные
    public class PostRequest
    {
        HttpWebRequest _request; // объек запроса
        string _address;

        public Dictionary<string, string> Headers { get; set; } // Для передачи заголовков

        public string Response { get; set; }
        public string Accept { get; set; }
        public string Host { get; set; }
        public string Date { get; set; } // передаём данные
        public string ContentType { get; set; }
        public WebProxy Proxy { get; set; }

        public PostRequest(string address)
        {
            _address = address;
            Headers = new Dictionary<string, string>();
        }

        public void Run() // Запускает созданный запрос
        {
            // создаём запрос, привести к HttpWebRequest
            _request = (HttpWebRequest)WebRequest.Create(_address);
            // указать, что данный запрос является Get запросом
            _request.Method = "POST";

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
            _request.Method = "POST";
            // перед выполнением запроса, cookieContainer запроса который мы передали в момент запроса
            _request.CookieContainer = cookieContainer;
            // адрес фидлера в запрос
            _request.Proxy = Proxy;
            _request.Accept = Accept;
            _request.Host = Host;
            // тип передаваемых данных на сервер
            _request.ContentType = ContentType;

            byte[] sentDate = Encoding.UTF8.GetBytes(Date);
            _request.ContentLength = sentDate.Length; // длина отправляемых данных
            Stream sendStream = _request.GetRequestStream();// добавляем в запрос
            sendStream.Write(sentDate, 0, sentDate.Length);
            sendStream.Close();


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