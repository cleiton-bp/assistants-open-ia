namespace assistants.Dtos;

public class CreateAssistantDto
{
    public string Name { get; set; } = "";
    public string Instructions { get; set; } = "";
    public string Model { get; set; } = "gpt-4o-mini";
}