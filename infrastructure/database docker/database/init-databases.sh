#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    CREATE DATABASE keycloak;
    CREATE DATABASE launchpad_candidates;
    
    CREATE USER keycloak WITH ENCRYPTED PASSWORD 'admin';
    CREATE USER candidates_api WITH ENCRYPTED PASSWORD 'admin';
    
    ALTER DATABASE keycloak OWNER TO keycloak;
    ALTER DATABASE launchpad_candidates OWNER TO candidates_api;
    
    \c keycloak
    GRANT ALL PRIVILEGES ON SCHEMA public TO keycloak;

    \c launchpad_candidates
    GRANT ALL PRIVILEGES ON SCHEMA public TO candidates_api;
EOSQL