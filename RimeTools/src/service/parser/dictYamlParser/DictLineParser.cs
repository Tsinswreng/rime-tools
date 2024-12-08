using model;
using model.consts;
using KVT = model.consts.KVType;
namespace service.parser.dictYamlParser;



public class DictLineParser
: I_lineStrToDictLine, I_dictLineToDictLineKVs, I_lineStrToDictLineKVs{
	//需要 metadata

	public DictLineParser(){}
	
	public DictLineParser(DictMetadata metadata){
		this.metadata = metadata;
	}


	public DictMetadata? metadata {get;set;}

	public i32 setMetadata(DictMetadata metadata){
		this.metadata = metadata;
		return 0;
	}


	protected i32 setBl(KV kv){
		if(metadata == null){
			throw new Exception("metadata is null");
		}
		kv.bl = BlPrefix.dictYaml+BlPrefix.delimiter+metadata.name;
		return 0;
	}

	/* 
少	stewʔ	90%
{	//字-碼
	id: 0
	,bl: "dict.yaml:dks"
	,kType: "TEXT"
	,kStr: "少"
	,kDesc: "text"
	,vType: "TEXT"
	,vStr: "stewʔ"
	,vDesc: "code"
}
	 */
	protected KV toText__codeKV(in DictLine line){
		var text__code = new KV();
		text__code.kStr = line.text;
		text__code.vType = KVT.STR.ToString();
		text__code.vStr = line.code;
		
		//text__code.kDesc = KDesc.
		// if(metadata?.name == null){
		// 	throw new Exception("metadata.name is null");
		// }
		// text__code.bl = BlPrefix.dictYaml+BlPrefix.delimiter+metadata.name;
		setBl(text__code);
		text__code.vDesc = VDesc.text.ToString();
		return text__code;
	}

/* 
少	stewʔ	90%
{	//頻--字-碼
	id:1
	,bl: "dict.yaml:dks"

	,kType: "INT"
	,kInt: 0
	,kDesc: "fKey"
	
	,vType: "TEXT"
	,vStr: "90%"
	,vDesc: "weight"
}
 */
	protected KV? toFKey__WeightKV(in DictLine line){
		if(line.weight == ""){
			return null;
		}
		var fKey__weight = new KV();
		//fKey__weight.kInt = 0; text__codeˇ加入表後取lastId作其值
		if(i64.TryParse(line.weight, out i64 weightI64)){//整數weight 作i64
			fKey__weight.vI64 = weightI64;
		}else{ //帶百分號之weight (ex: 10%) 作字串
			fKey__weight.vStr = line.weight;
		}
		setBl(fKey__weight);
		fKey__weight.kType = KVType.I64.ToString();
		fKey__weight.kDesc = KDesc.fKey.ToString();
		fKey__weight.vDesc = VDesc.weight.ToString();
		fKey__weight.vType = KVT.STR.ToString();
		return fKey__weight;
	}


	//ref readonly 
	public DictLineKVs dictLineToDictLineKVs(in DictLine line){
		var text__code = toText__codeKV(line);
		var fKey__weight = toFKey__WeightKV(line);
		if(fKey__weight == null){
			return new DictLineKVs(text__code);
		}
		return new DictLineKVs(text__code, fKey__weight);
	}

	public DictLine lineStrToDictLine(str line){
		var items = line.Split("\t");
		if(metadata?.columns != null){
			//TODO
			throw new NotImplementedException();
		}
		var ans = new DictLine();
		ans.text = items.ElementAtOrDefault(0)??"";
		ans.code = items.ElementAtOrDefault(1)??"";
		ans.weight = items.ElementAtOrDefault(2)??"";
		return ans;
	}

	public DictLineKVs lineStrToDictLineKVs(str line){
		var lineObj = lineStrToDictLine(line);
		var kvs = dictLineToDictLineKVs(lineObj);
		return kvs;
	}

}