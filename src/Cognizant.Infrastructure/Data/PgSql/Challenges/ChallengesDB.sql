--
-- DATABASE RESTORE
--

DROP DATABASE IF EXISTS cognizant;

CREATE DATABASE cognizant WITH TEMPLATE = template0 ENCODING = 'UTF8';

ALTER DATABASE cognizant OWNER TO postgres;

--
-- CONNECT
--

\connect cognizant

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- DATABASE DEFINITION
--


-- TABLE: challenge
--

DROP TABLE IF EXISTS public.challenge CASCADE;

CREATE TABLE public.challenge (
    id SERIAL PRIMARY KEY,
    task_name character varying NOT NULL,
    description character varying NOT NULL,
	input_param character varying NOT NULL,
	output_param character varying NOT NULL
);

ALTER TABLE public.challenge OWNER TO postgres;

-- SEED: challenge
INSERT INTO public.challenge (task_name, description, input_param, output_param) VALUES ('Fibonacci', 'Calculate what is the xth fibonacci number and output it to console in a single line with only the result. Input is the specific fibonacci number we want to output.', '10', '55');
INSERT INTO public.challenge (task_name, description, input_param, output_param) VALUES ('Multiples of 3 and 5', 'If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23. Find the sum of all the multiples of 3 or 5 below 1000. Input value is the range (1000).', '1000', '233168');
INSERT INTO public.challenge (task_name, description, input_param, output_param) VALUES ('Largest prime factor', 'The prime factors of 13195 are 5, 7, 13 and 29. What is the largest prime factor of a given number? Given number is the input value.', '600851475143', '6857');

-- REFERENCE TABLE: user
--

DROP TABLE IF EXISTS public.user CASCADE;

CREATE TABLE public.user (
	id SERIAL PRIMARY KEY,
	name character varying NOT NULL,
	success_solutions integer NOT NULL,
	tasks character varying NOT NULL
);

ALTER TABLE public.user OWNER TO postgres;

-- SEED: user
-- INSERT INTO public.user (name, success_solutions, tasks) VALUES ('Justas', 1, 'Fibonacci');
