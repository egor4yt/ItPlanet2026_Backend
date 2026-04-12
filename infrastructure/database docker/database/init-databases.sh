#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    CREATE DATABASE keycloak;
    CREATE USER keycloak WITH ENCRYPTED PASSWORD 'admin';
    ALTER DATABASE keycloak OWNER TO keycloak;
    \c keycloak
      GRANT ALL PRIVILEGES ON SCHEMA public TO keycloak;
    
    CREATE DATABASE launchpad_candidates;
    CREATE USER candidates_api WITH ENCRYPTED PASSWORD 'admin';
    ALTER DATABASE launchpad_candidates OWNER TO candidates_api;
    \c launchpad_candidates
      GRANT ALL PRIVILEGES ON SCHEMA public TO candidates_api;
    
    CREATE DATABASE launchpad_warehouse;
    CREATE USER warehouse_api WITH ENCRYPTED PASSWORD 'admin';
    ALTER DATABASE launchpad_warehouse OWNER TO warehouse_api;
    \c launchpad_candidates
        GRANT ALL PRIVILEGES ON SCHEMA public TO warehouse_api;

EOSQL