using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string url = @"http://192.168.1.102:8080/";

        using var mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var server = "www.google.com";
        mySocket.Connect(server, 80);

        using var stream = new NetworkStream(mySocket);
        // отправляем сообщение для отправки
        var message = $"GET / HTTP/1.1\r\nHost: {server}\r\nConnection: Close\r\n\r\n";
        // кодируем его в массив байт
        var data = Encoding.UTF8.GetBytes(message);
        // отправляем массив байт на сервер 
        await stream.WriteAsync(data);

        // буфер ддя получения данных
        var responseData = new byte[512];
        // StringBuilder для склеивания полученных данных в одну строку
        var response = new StringBuilder();
        int bytes;  // количество полученных байтов
        do
        {
            // получаем данные
            bytes = await stream.ReadAsync(responseData);
            // преобразуем в строку и добавляем ее в StringBuilder
            response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
        }
        while (bytes > 0); // пока данные есть в потоке 

        // выводим данные на консоль
        Console.WriteLine(response);
    }
}
