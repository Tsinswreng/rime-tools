/* =異步讀取一行文字 */
namespace service.parser;

public interface I_ReadLine{
	Task<str?> ReadLine();
}

