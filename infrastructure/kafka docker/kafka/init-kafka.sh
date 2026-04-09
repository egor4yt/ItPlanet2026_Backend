#!/bin/bash

echo "Waiting for Kafka to be ready..."
cub kafka-ready -b localhost:9092 1 20

kafka-topics --bootstrap-server localhost:9092 --create --if-not-exists --topic skill-created --partitions 1 --replication-factor 1
kafka-topics --bootstrap-server localhost:9092 --create --if-not-exists --topic skill-verified --partitions 1 --replication-factor 1

echo "Topics created."