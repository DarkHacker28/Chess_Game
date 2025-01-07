 # Chess Game

## Overview
This project is a Chess Game implementation built using a combination of C#, C, and C++ libraries. The goal of this project is to provide an engaging and functional chess-playing experience with features like move validation, AI opponent, and game-saving functionality.

---

## Features
- **Interactive Chessboard:** A user-friendly interface for making moves.
- **Move Validation:** Ensures only legal chess moves are allowed.
- **AI Opponent:** A challenging computer player powered by efficient algorithms.
- **Game Save/Load:** Save your progress and resume later.
- **Multilingual Codebase:** Leverages the strengths of C#, C, and C++.
- **Cross-Platform Compatibility:** Runs on Windows, Linux, and macOS (with proper dependencies).

---

## Prerequisites

### Tools and Libraries
- **C# Compiler** (e.g., Visual Studio, .NET Core)
- **C Compiler** (e.g., GCC or Clang)
- **C++ Compiler** (e.g., GCC, Clang, or MSVC)
- **CMake** (for building the C and C++ components)
- **.NET Core SDK** (for running the C# components)

### Installation
Ensure the following are installed on your system:
- .NET SDK: [Download .NET SDK](https://dotnet.microsoft.com/download)
- GCC or Clang for compiling C and C++ libraries.
- CMake: [Download CMake](https://cmake.org/download/)

---

## Project Structure
```
ChessGame/
├── src/
│   ├── ChessGUI.cs         # Chessboard interface written in C#
│   ├── MoveValidator.c      # C library for move validation
│   └── AIEngine.cpp         # C++ library for AI opponent
├── build/
│   └── binaries           # Compiled binaries for C and C++ components
├── README.md              # Project documentation
├── CMakeLists.txt         # CMake configuration
├── ChessGame.sln          # Visual Studio solution
└── .gitignore            # Ignored files
```

---

## How to Build

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/ChessGame.git
cd ChessGame
```

### 2. Build C and C++ Libraries
Use CMake to configure and build the libraries:
```bash
mkdir build && cd build
cmake ..
make
```
This will generate the necessary binaries in the `build/binaries` directory.

### 3. Run the C# Application
Open the project in Visual Studio or your preferred IDE and build the C# solution. Alternatively, use the command line:
```bash
cd src
 dotnet build
 dotnet run
```

---

## Usage
- **Start the game:** Run the compiled Chess Game application.
- **Play:** Use the GUI to make moves. The AI will respond if playing against the computer.
- **Save/Load:** Save the current game state to a file and load it later.

---

## Contributing
We welcome contributions! To contribute:
1. Fork the repository.
2. Create a new branch.
3. Make your changes.
4. Submit a pull request.

---

## License
This project is licensed under the MIT License. See the LICENSE file for details.

---

## Future Enhancements
- Add support for networked multiplayer mode.
- Enhance AI difficulty levels.
- Implement move animations in the GUI.
- Add support for additional languages for UI and documentation.

Enjoy playing Chess!

