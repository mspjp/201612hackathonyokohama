var sys = require('sys');

var docomo = require('./docomoapi.js');
docomo.key='YOUR KEY';

docomo.similarity('Tsukuba', 'つくば', function(s){
  if(s == -1)sys.log('エラー');
  else sys.log('Tsukuba-つくば:類似度:', s);
});

docomo.similarity('yokkaichi', '四日市', function(s){
  sys.log('yokkaichi-四日市:類似度:', s);
});

docomo.entity('水野氏が設計した日本株式会社 東京本社の完成記念式典を10月10日　20:15から行う', null, function(r){
  sys.log('「水野氏が設計した日本株式会社 東京本社の完成記念式典を10月10日　20:15から行う」 固有表現抽出:',r);
});

docomo.entity('横浜ハッカソン@10月10日', 'DAT', function(r){
  sys.log('横浜ハッカソン@10月10日:',r);
});

docomo.entity('横浜ハッカソン@10月10日', null, function(r){
  sys.log('横浜ハッカソン@10月10日(ALL):',r);
});

docomo.hiragana('こにゃにゃちわ〜', 'katakana', function(r){
  sys.log('こにゃにゃちわ〜:',r);
});

docomo.hiragana('魑魅魍魎', 'hiragana', function(r){
  sys.log('魑魅魍魎:',r);
});
docomo.hiragana('Step By Step システム構築ガイド', 'hiragana', function(r){
  sys.log('Step By Step システム構築ガイド:',r);
});

docomo.morph('今日の天気は、晴のち曇。もしくは雨か雪。', 'form|pos|read', '', function(r){
  sys.log('今日の天気は、晴のち曇。もしくは雨か雪。:',r);
});

docomo.morph('猫のキティーが膝の上に乗っている', 'form|pos', '名詞|動詞語幹', function(r){
  sys.log('猫のキティーが膝の上に乗っている:',r);
});
