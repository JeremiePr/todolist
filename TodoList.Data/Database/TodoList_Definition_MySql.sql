drop table objective_history;
drop table task;
drop table objective;
drop table status_type;

create table status_type 
(
	type_key int primary key not null,
    type_name varchar(20) not null
);

create table objective
(
	id int primary key auto_increment,
    title varchar(100) not null,
    details varchar(1000) null,
    priority int not null,
    status_type_key int not null,
    status_date datetime null,
    status_details varchar(1000) null
);

alter table objective
add constraint fk_objective_status_type_key
foreign key (status_type_key)
references status_type(type_key);

create table task
(
	id int primary key auto_increment,
    objective_id int not null,
    title varchar(100) not null,
    details varchar(1000) null,
    priority int not null,
    status_type_key int not null
);

alter table task
add constraint fk_task_objective_id
foreign key (objective_id)
references objective(id);

alter table task
add constraint fk_task_status_type_key
foreign key (status_type_key)
references status_type(type_key);

create table objective_history
(
	id int primary key auto_increment,
    objective_id int not null,
    previous_status_type_key int null,
    current_status_type_key int not null,
    update_date datetime not null,
    is_new bit not null
);

alter table objective_history
add constraint fk_objective_history_objective_id
foreign key (objective_id)
references objective(id);

alter table objective_history
add constraint fk_objective_history_previous_status_type_key
foreign key (previous_status_type_key)
references status_type(type_key);

alter table objective_history
add constraint fk_objective_history_current_status_type_key
foreign key (current_status_type_key)
references status_type(type_key);