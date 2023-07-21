# Assistant
## 概要
IRIAMという配信サービスのアプリを便利に使うためのツールです
ミラーリングアプリ（LetsView）と連携し、コメントの取得や読み上げを行う事ができます

## 必要なもの
- IriamAssistant.exe(ビルド成果物)
- LetsView
- [棒読みちゃん](https://chi.usamimi.info/Program/Application/BouyomiChan/)
- [VOICEVOX](https://voicevox.hiroshiba.jp/)

## 使い方
1. 棒読みちゃんを起動し[VOICEVOXと連携](https://vip-jikkyo.net/voicevox-bouyomichan)
2. LetsViewを起動しiPhoneのIRIAMをミラーリング
4. Ｗindow選択でLetsViewを選択 (二つある場合はどちらかがミラーリング画面です)
5. 適当な配信を閲覧(自動更新されます）

## 既知の問題点
- 誤読が多い
- 何度も同じ文章を読み上げる
- 絵文字を別の漢字として読み取ろうとする
- LetsViewの仕様変更に伴いBorderless Windowが検索対象にでなくなった（実質的に使用不可状態）
- LetsViewでのミラーリング中に音声出力をミュートできない場合がある

## 改善予定（期日未定）
- Borderless Windowを検出できるようにする
- Airplayを内部でミラーリングできるようにする
- Nox等のアンドロイドエミュレーターと連携できるようにする
- 字句解析をコメント毎に分割する
- 字句解析結果の既存文章との比較の一致率を下げ、1文字違い等は再度読み上げないようにする
- 字句解析エンジンをTesseractに変更する
- 絵文字を漢字に誤読しないように教育する
- 行の初めに類出する単語を名前として分類するようにする
- 名前によって読み上げの声を変える
- 特殊コメントを読み上げる代わりにSEを鳴らす
