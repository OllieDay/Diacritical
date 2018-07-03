module Diacritical

    open System.Text.RegularExpressions

    let private pattern = "(shuang|chuang|zhuang|xiang|qiong|shuai|niang|guang|sheng|kuang|shang|jiong|huang|jiang|shuan|xiong|zhang|zheng|zhong|zhuai|zhuan|qiang|chang|liang|chuan|cheng|chong|chuai|hang|peng|chuo|piao|pian|chua|ping|yang|pang|chui|chun|chen|chan|chou|chao|chai|zhun|mang|meng|weng|shai|shei|miao|zhui|mian|yong|ming|wang|zhuo|zhua|shao|yuan|bing|zhen|fang|feng|zhan|zhou|zhao|zhei|zhai|rang|suan|reng|song|seng|dang|deng|dong|xuan|sang|rong|duan|cuan|cong|ceng|cang|diao|ruan|dian|ding|shou|xing|zuan|jiao|zong|zeng|zang|jian|tang|teng|tong|bian|biao|shan|tuan|huan|xian|huai|tiao|tian|hong|xiao|heng|ying|jing|shen|beng|kuan|kuai|nang|neng|nong|juan|kong|nuan|keng|kang|shua|niao|guan|nian|ting|shuo|guai|ning|quan|qiao|shui|gong|geng|gang|qian|bang|lang|leng|long|qing|ling|luan|shun|lian|liao|zhi|lia|liu|qin|lun|lin|luo|lan|lou|qiu|gai|gei|gao|gou|gan|gen|lao|lei|lai|que|gua|guo|nin|gui|niu|nie|gun|qie|qia|jun|kai|kei|kao|kou|kan|ken|qun|nun|nuo|xia|kua|kuo|nen|kui|nan|nou|kun|jue|nao|nei|hai|hei|hao|hou|han|hen|nai|rou|xiu|jin|hua|huo|tie|hui|tun|tui|hun|tuo|tan|jiu|zai|zei|zao|zou|zan|zen|eng|tou|tao|tei|tai|zuo|zui|xin|zun|jie|jia|run|diu|cai|cao|cou|can|cen|die|dia|xue|rui|cuo|cui|dun|cun|cin|ruo|rua|dui|sai|sao|sou|san|sen|duo|den|dan|dou|suo|sui|dao|sun|dei|zha|zhe|dai|xun|ang|ong|wai|fen|fan|fou|fei|zhu|wei|wan|min|miu|mie|wen|men|lie|chi|cha|che|man|mou|mao|mei|mai|yao|you|yan|chu|pin|pie|yin|pen|pan|pou|pao|shi|sha|she|pei|pai|yue|bin|bie|yun|nüe|lve|shu|ben|ban|bao|bei|bai|lüe|nve|ren|ran|rao|xie|re|ri|si|su|se|ru|sa|cu|ce|ca|ji|ci|zi|zu|ze|za|hu|he|ha|ju|ku|ke|qi|ka|gu|ge|ga|li|lu|le|qu|la|ni|xi|nu|ne|na|ti|tu|te|ta|xu|di|du|de|bo|lv|ba|ai|ei|ao|ou|an|en|er|da|wu|wa|wo|fu|fo|fa|nv|mi|mu|yi|ya|ye|me|mo|ma|pi|pu|po|yu|pa|bi|nü|bu|lü|e|o|a)r?[1-5]"

    let private vowels = ['a'; 'e'; 'i'; 'o'; 'u'; 'ü'; 'A'; 'E'; 'I'; 'O'; 'U'; 'Ü']

    let private diacritics = [
        ['ā'; 'ē'; 'ī'; 'ō'; 'ū'; 'ǖ'; 'ǖ'; 'Ā'; 'Ē'; 'Ī'; 'Ō'; 'Ū'; 'Ǖ'];
        ['á'; 'é'; 'í'; 'ó'; 'ú'; 'ǘ'; 'ǘ'; 'Á'; 'É'; 'Í'; 'Ó'; 'Ú'; 'Ǘ'];
        ['ǎ'; 'ě'; 'ǐ'; 'ǒ'; 'ǔ'; 'ǚ'; 'ǚ'; 'Ǎ'; 'Ě'; 'Ǐ'; 'Ǒ'; 'Ǔ'; 'Ǚ'];
        ['à'; 'è'; 'ì'; 'ò'; 'ù'; 'ǜ'; 'ǜ'; 'À'; 'È'; 'Ì'; 'Ò'; 'Ù'; 'Ǜ'];
        ['a'; 'e'; 'i'; 'o'; 'u'; 'ü'; 'ü'; 'A'; 'E'; 'I'; 'O'; 'U'; 'Ü']]

    let private positions = [
        ("ai", "a*i");
        ("ao", "a*o");
        ("ei", "e*i");
        ("ia", "ia*");
        ("iao", "ia*o");
        ("ie", "ie*");
        ("io", "io*");
        ("iu", "iu*");
        ("AI", "A*I");
        ("AO", "A*O");
        ("EI", "E*I");
        ("IA", "IA*");
        ("IAO", "IA*O");
        ("IE", "IE*");
        ("IO", "IO*");
        ("IU", "IU*");
        ("ou", "o*u");
        ("ua", "ua*");
        ("uai", "ua*i");
        ("ue", "ue*");
        ("ui", "ui*");
        ("uo", "uo*");
        ("ve", "üe*");
        ("üe", "üe*");
        ("OU", "O*U");
        ("UA", "UA*");
        ("UAI", "UA*I");
        ("UE", "UE*");
        ("UI", "UI*");
        ("UO", "UO*");
        ("VE", "ÜE*");
        ("ÜE", "ÜE*");
        ("a", "a*");
        ("e", "e*");
        ("i", "i*");
        ("o", "o*");
        ("u", "u*");
        ("v", "ü*");
        ("ü", "ü*");
        ("A", "A*");
        ("E", "E*");
        ("I", "I*");
        ("O", "O*");
        ("U", "U*");
        ("V", "Ü*")
        ("Ü", "Ü*")]

    let private replace (oldValue : string) newValue (text : string) =
        text.Replace (oldValue, newValue)

    let private createVowelWithDiacritic vowel tone =
        let toneIndex = tone - 1
        let toneVowels = List.item toneIndex diacritics
        let vowelIndex = List.findIndex ((=) vowel) vowels
        List.item vowelIndex toneVowels

    let private convertToPinyin (text : string) =
        let toneIndex = text.Length - 1
        let tone = text.Substring toneIndex |> int
        let word = text.Substring (0, toneIndex)
        let (vowels, vowelsWithPosition) = List.find (fun (vowels, _) -> word.Contains vowels) positions
        let vowelsToReplace = Regex.Match(vowelsWithPosition, ".\\*").Value
        let vowelWithDiacritic = createVowelWithDiacritic vowelsToReplace.[0] tone
        word
            |> replace vowels vowelsWithPosition
            |> replace vowelsToReplace (string vowelWithDiacritic)

    let private matchText text =
        Regex.Matches (text, pattern, RegexOptions.IgnoreCase)
            |> Seq.cast<Match>
            |> Seq.toList
            |> List.map string

    let rec private convertText text = function
        | [] -> text
        | head :: tail ->
            let pinyin = convertToPinyin head
            let replacedText = text |> replace head pinyin
            convertText replacedText tail

    let convert text =
        text |> matchText |> convertText text
