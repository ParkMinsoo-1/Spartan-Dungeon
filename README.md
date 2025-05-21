# Spartan-Dungeon

---
# 🧩 Unity Item Interaction System


## ✨ 주요 기능

### 🎮 1. 기본 이동 및 점프
- 플레이어는 `W, A, S, D` 키를 사용하여 이동 할 수 있으며, `Space` 키 입력으로 점프를 사용할 수 있습니다.
- 마우스 포인트에 따라 플레이어의 카메라 각도를 조절 할 수 있습니다.
- `E` 키 입력으로 상호작용을 수행합니다.

### 📦 2. UI
- 체력, 스테미나를 UI 처리하여 플레이어의 변화하는 체력과 스테미나를 실시간으로 표시합니다.
- 아이템의 정보를 마우스 포인트 근처에서 보여줍니다.

### 📦 3. 동적 환경 조사
- MousePosition에서 Raycast를 활용하여 해당 오브젝트의 정보를 표시합니다.
- 정보를 확인 할 수 있는 오브젝트는 ItemObject로 한정지었으며, ItemObject의 경우 SciptableObject를 사용하였습니다.

### ❤️ 4. 아이템 데이터 및 아이템 사용
- ScriptableOjbect로 정의한 ItemObejct의 경우 각각의 ItemData를 가지고 있습니다.
- 해당 아이템과 상호작용을 할 경우 각 ItemObject가 가지고 있는 ItemData에 따라 회복의 종류가 결정됩니다.
- 소비 아이템은 종류에 따라 체력과 스태미너를 회복 할 수 있습니다.

---

## 🏗️ 코드 구조

### 📁 Scripts
```plaintext
├── Player
│ ├── CharacterManager.cs
│ ├── LookItemInfo.cs // 플레이어의 마우스 포인트에 따라 해당 정보를 받을 수 있도록 해줌.
│ ├── Player.cs
│ ├── PlayerController.cs // 플레이어의 이동을 위한 스크립트.
│ ├── ResourceController.cs // 플레이어의 체력 및 스태미나를 조절해주기 위함.
│
├── Items
│ ├── ItemData.cs // ScriptableObject 정의
│ ├── ItemObject.cs // 게임 오브젝트에 붙는 아이템
│
├── UI
│ ├── ItemInfoUI.cs // 마우스 포인트에 뜨는 UI
│ ├── Resource.cs // 각각의 자원에 연결 (health,stamina)
│ ├── ResourceManager.cs // 각 자원에 접근 할 수 있도록 해줌.
```

## 🛠️ 사용 기술

- `InputSystem` (플레이어 이동, 점프 및 E 키 상호작용)
- `ScriptableObject` 기반 아이템 구조
- `Physics.Raycast`, `OnTriggerEnter` 등 상호작용 감지 방식
