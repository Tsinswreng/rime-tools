using model;
using service;
using service.phraseMker;
using tools;
namespace ctrler;


class Splitter: I_SplitByCodePoint{
	//TODO temp impl
	public IList<str> splitByCodePoint(str str){
		var ans = new List<str>();
		for(int i=0; i<str.Length; i++){
			ans.Add(str[i].ToString());
		}
		return ans;
	}
}

/// <summary>
/// temp 
/// </summary>
public class MkPhrase{

	public MkPhrase(){

	}

	public I_getNext<I_KV> wordFreqReader{get;set;} = (I_getNext<I_KV>)new WordFreqReader();
	public I_mkPhrase phraseMkr{get;set;} = new PhraseMker_HeadEtTail();
	public I_seekCode codeSeeker{get;set;} = new ReverseLookup();

	protected I_SplitByCodePoint _splitter = new Splitter();

	//TODO 改成流式處理
	public code mkPhrase(){
		var word__code__freq_s = new List< List<str> >();// [str, str, str][]
		for(var i = 0;wordFreqReader.hasNext();i++){
			var wordFreq = wordFreqReader.getNext();
			if(wordFreq == null){break;}
			var word = wordFreq.kStr; //單字或詞 以 「車輛」 潙例
			if(word == null || word==""){continue;}
			var freq = wordFreq.vI64;
			var wordList = _splitter.splitByCodePoint(word??"");// ["車","輛"]
			var codes = codeSeeker.seekCode(wordList);//[ ["che1","liang4"], ["ju1", "liang4"] ]
			if(codes.Count == 0){
				continue;
			}
			var u_word__code__freq = new List<str>();// [str, str, str]
			for(var j = 0; j < codes.Count; j++){
				var codeList = codes[j]; // ["che1","liang4"]
				var phraseCode = phraseMkr.mkPhrase(codeList);// 假設無処理: ["che1","liang4"]
				if(phraseCode == null || phraseCode.Count == 0){
					continue;
				}
				var joinedPhraseCode = string.Join("", phraseCode); // "che1liang4"
				var ua = new List<str>(){word??"", joinedPhraseCode, freq?.ToString()??""};// ["車輛", "che1liang4", "1000"]
				u_word__code__freq.Add(string.Join("\t", ua));
			}
			word__code__freq_s.Add(u_word__code__freq);
		}
		
		// output
		for(var i = 0;i<word__code__freq_s.Count;i++){
			var u_word__code__freq = word__code__freq_s[i];
			for(var j = 0;j<u_word__code__freq.Count;j++){
				var line = u_word__code__freq[j];
				Console.WriteLine(u_word__code__freq[j]);
			}
		}
		return 0;
	}
}