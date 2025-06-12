// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using OpenAI.Chat;
using OpenAIChatENG;
using System.Text;

var modelName = "gpt-4o";
var client = new ChatClient(modelName, Constants.OpenAIKey);

Console.WriteLine("AI: Hello! You can ask me anything or press Enter to exit.");
Console.WriteLine();

var messages=new List<ChatMessage>();

while (true)
{
    var sb = new StringBuilder();

    Console.WriteLine("You: ");
    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
    {
        break;
    }

    messages.Add(new UserChatMessage(input));
    Console.WriteLine();

    //var response =await client.CompleteChatAsync(messages);
    //var aiResponse = response.Value.Content[0].Text;
    //Console.WriteLine($"AI: {aiResponse}");

    Console.WriteLine("AI: ");
    var stream = client.CompleteChatStreamingAsync(messages);
    Console.WriteLine();
    await foreach (var update in stream)
    {
     foreach(var content in update.ContentUpdate)
        {
            Console.Write(content.Text);
            sb.Append(content.Text);
        }
    }

    messages.Add(new AssistantChatMessage(sb.ToString()));

    Console.WriteLine();

    //messages.Add(new AssistantChatMessage(aiResponse));
}

