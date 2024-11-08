namespace service.phraseMker;

/* 
bug: 剝奪 只有pklt 無 pkdt

正式 正ˋ無去聲
 */

/// <summary>
/// 造詞策略: 取每字之首末兩碼
/// 只用于二字詞
/// </summary>
public class PhraseMker_HeadEtTail : I_mkPhrase{

	protected str _handleCode(str code){
		return $"{code[0]}{code[code.Length - 1]}";
	}

	public IList<str> mkPhrase(IList<str> codes){
		var ans = new List<str>();
		if(codes.Count != 2){
			//throw new ArgumentException("PhraseMker_HeadEtTail can only handle two-word phrases.");
			return ans;
		}
		for(var i = 0; i < codes.Count; i++){
			var code = codes[i];
			if(code == null || code.Length == 0){
				continue;
			}
			ans.Add(_handleCode(code));
		}
		return ans;
	}
}