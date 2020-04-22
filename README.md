# wpf_training

WPF applicationの自習用repository

色んなサイトのサンプルアプリを通じて、WPF applicationの作り方を学んでいます。

## Contents

>:construction:工事中

### sample1

[WPF Prism 入門エントリ](https://elf-mission.net/wpf-prism-index/)をもとに作っているprogram

解説されているプログラムとは、project設定の違いと使用している.Net platformの違いを除いてほぼ同じプログラムになっています。

### sample2

[【改訂版】PrismとReactivePropertyで簡単MVVM！](https://qiita.com/hiki_neet_p/items/e04b5ac692aa18df0968)をもとに作ったprogram

相違点は以下の通り：
- Modelの実装なし
  - リンク先のとは違って、入力値もmodelで保持し、DI containerを通じてviewmodelに渡そうとした。しかしviewmodelには全く反映されなかった。
  - おそらくDI containerの使い方と、model<-->viewmodel間の通信方法をまだ理解できていないのが原因だと思う。
  - 別なsample programで通信方法を習得して、できるようになったらこのprogramにも反映させてみる。
- IdialogServiceを用いたdialogを使用
  - 実装は[WPF Prism episode: 16 ～ Prism7.2、ダイアログは IDialogService でって言ったよね！ ～](https://elf-mission.net/programming/wpf/episode16/)をほぼ踏襲。
  
### sample3

[WPF アプリを MVVM で作る](https://qiita.com/seka/items/50d253c824e8914f937e)をもとに作成したprogram

元ネタはLibraryを一切使わずに作成していた。その上modelの捉え方がなんか変に感じた(control=modelとしてしまっている？)。
Prism+ReactivePropertyで実装しなおせばかなり簡単になりそうだったので、それで作り直してみた。

**結果:**
たしかに簡単に作成することができた。ついでに非同期I/Oを使ってみた。またfont sizeを変えられるようにもした。

ただModelの設計がうまく行かなかった (RegisterTypeでinterfaceと一緒に登録したかったが、うまく動かなかった)。[こちらのサイト](https://elf-mission.net/wpf-prism-index/)で一通り学習してから、modelを作り直してみることにする。

### sample4

[Nine Works　asyncとIProgressを使ってプログレスバーを操作する](http://nineworks2.blog.fc2.com/blog-entry-4.html)をもとに作成したprogram

Livetを使用していた。その部分だけをPrism+ReactivePropertyで書き換えた。

全部実装し終えたと思ったら、なぜかViewModelLocatorで実行時エラーが発生してしまった。そのあたりを重点的に、sample3のコードと突き合わせて間違いを探してみたが、全くわからなかった。仕方なく前のcommitまで巻き戻し、その後少しずつ実行とcommitを繰り返した。

その結果、原因がData BindingのValueのつけ忘れだということがわかった。こッッッんな初歩的なミスに気づかないとか……。まあよくあることだけど。

今後はちゃんと
1. 少し実装
2. **コードの実行**
3. commit
を繰り返すようにしよう。
