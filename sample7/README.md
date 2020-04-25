# No Name

> Sorry, Japanese only yet.

タスク管理・記録・分析tool

名前はまだない。

## Features

- Task list Project
  - Tasks を一つにまとめたもの
  - 時間帯はproject単位で指定する
  - allow to nest
- Recorder
  - Userが自信の実際の行動を記録できる
  - Projectと比較して、何ができて何ができなかったかを自動で評価し、統計をとる

## For developers

### Database schemes

```sql
create table if not exists Tasks( Id integer not null primary key autoincrement, Summary text not null, Description text, Length integer, Deadline integer, IsCompleted real,  Status text, Priority integer, Location text, CreatedAt integer not null, UpdatedAt integer not null)
create table if not exists Projects( Id integer not null primary key autoincrement, Name text not null, Begin integer,End integer,Status text,Priority integer,CreatedAt integer not null,UpdatedAt integer not null )
create table if not exists Records( Id integer not null primary key autoincrement, IsCompleted integer not null default 0 check(IsCompleted in (0,1)),Begin integer, End integer,TaskId integer not null, Location text, CommitMessage text not null, foreign key(TaskId) references Tasks(Id))
create table if not exists ReferencePaths( Id integer not null primary key autoincrement, Path text not null )
create table if not exists ProjectReferencing( ProjectId integer,ReferenceId integer,foreign key(ProjectId) references Projects(Id), foreign key(ReferenceId) references ReferencePaths(Id))
create table if not exists TaskBelonging( TaskId integer,ProjectId integer, foreign key(TaskId) references Tasks(Id),foreign key(ProjectId) references Projects(Id))
create table if not exists Tags( Id integer not null primary key autoincrement, Name text unigue not null )
create table if not exists TaskTagging( TaskId integer, TagId integer, foreign key(TaskId) references Tasks(Id), foreign key(TagId) references Tags(Id) )
create table if not exists ProjectTagging( ProjectId integer, TagId integer, foreign key(ProjectId) references Projects(Id), foreign key(TagId) references Tags(Id) )
create table if not exists RecordTagging( RecordId integer, TagId integer, foreign key(RecordId) references Records(Id), foreign key(TagId) references Tags(Id) )
```
