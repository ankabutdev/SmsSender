using RestSharp;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Source https://www.infobip.com/docs/sms/send-message

        var API_KEY = "";
        var BASE_URL = "";
        var SENDER = "";
        var DESTINATION = "";
        var MESSAGE_TEXT = "Be water my friend!";

        var options = new RestClientOptions(BASE_URL)
        {
            MaxTimeout = -1,
        };
        var client = new RestClient(options);
        var request = new RestRequest($"{BASE_URL}/messages-api/1/messages", Method.Post);
        request.AddHeader("Authorization", API_KEY);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");

        var body = $@"
                {{
                    ""messages"":
                    [
                        {{
                            ""channel"":""SMS"",
                            ""sender"":""{SENDER}"",
                            ""destinations"":
                            [
                                {{
                                    ""to"":""{DESTINATION}""
                                }}
                            ],
                            ""content"":
                            {{
                                ""body"":
                                {{
                                    ""text"":""{MESSAGE_TEXT}"",
                                    ""type"":""TEXT""
                                }}
                            }}
                        }}
                    ]
                }}";

        request.AddStringBody(body, DataFormat.Json);
        RestResponse response = await client.ExecuteAsync(request);
        Console.WriteLine(response.Content);
    }
}
