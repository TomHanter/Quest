Чего вы хотите?

*[Привет] Я просто хотел сказать привет.

*[Не будьте грубыми] Не нужно быть таким грубым..

-
-> Divert1

== Divert1 ===
Ты тратишь мое время. У меня сегодня много дел.
* [О пещере] Вы знаете что-нибудь о той пещере?
     Я знаю, что несколько лет назад отец Кадена пытался сходить за сокровищами. Но он так и не вернулся.

** [Вернуться] -> Divert1
* [О городе] Что сейчас происходит в городе?
 Сегодня ничего. Как и каждый день.
** [Вернуться] -> Divert1
* [Об озере] Вы можете рассказать мне об озере?
Я потеряла там свой любимый браслет много лет назад... Не могли бы вы сходить за ним?
** [Да] Думаю, я смогу вписать его в свое расписание.
** [Нет] Нет, я не хочу этого делать.
-- -> Divert1
* [О монстрах] Разве вы не замечали здесь странных древесных монстров? Разве не страшно?
Какие монстры? Я не вижу никаких монстров.
** [Вот здесь] Они прямо здесь. Как ты можешь их не видеть?
Я не понимаю, о чем ты говоришь.
*** [Вернуться] -> Divert1
* [Оооооокей] Конечно, нет.
*** [Вернуться] ->Divert1
* [Покинуть] Простите, я не хотел с вами разговаривать.
** [Продолжить]->EndPart

== EndPart ==

->END