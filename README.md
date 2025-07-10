# RFID IoT Backend Tools

A collection of .NET-based tools designed to handle, test, and store RFID data communication between readers, backend systems, and warehouses using MQTT and MongoDB.

---

## 🧠 Overview

This repository contains three major components that work together in a simulated RFID warehouse environment:

### 1. 🔌 MQTT Engine

Acts as the **core backend processor**. It:

- Subscribes to incoming MQTT topics where RFID readers publish scanned tag data.
- Uses a dynamic **mapping system** to determine where to route tag data.
- Publishes processed tag data to appropriate MQTT topics based on business logic and mappings.
- Ensures smooth and real-time warehouse operation by handling message flow between readers and subscriber applications.

### 2. ⚙️ Load Tester

A simulation tool used to **stress-test the MQTT engine**. It:

- Simulates 50+ RFID readers sending data concurrently.
- Helps verify the stability, scalability, and performance of the system.
- Can be used to test scenarios such as burst loads, parallel scans, and prolonged reader activity.

### 3. 🛢️ Mongo Dumper

Acts as a **data backup system**. It:

- Listens to the same incoming data stream (whether published or not).
- Dumps all incoming raw tag data into a MongoDB database for traceability and backup.
- Ensures that no data is lost, even if there are issues in publishing to topics.

---

## 📁 Project Structure

```plaintext
rfid-iot-backend-tools/
├── mqtt-engine/       # Main message router using MQTT
├── load-tester/       # Multi-reader simulation for stress testing
├── mongo-dumper/      # Data storage layer using MongoDB
