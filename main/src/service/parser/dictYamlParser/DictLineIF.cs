using model;
namespace service.parser.dictYamlParser;

public interface I_convertLineObjToKV{
	KV convert(DictLine line);
}

public interface I_parseLineStr{
	DictLine parseLineStr(str line);
}