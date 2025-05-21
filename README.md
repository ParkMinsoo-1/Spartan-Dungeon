# Spartan-Dungeon

---
# 🧩 Unity Item Interaction System

Unity 기반으로 제작된 **아이템 상호작용 및 자원 회복 시스템**입니다.  
플레이어는 게임 내 아이템과 상호작용하여 체력 또는 스태미너를 회복할 수 있으며, 마우스를 통한 정보 확인 기능도 포함되어 있습니다.

---

## ✨ 주요 기능

### 🎮 1. 플레이어 상호작용 시스템
- `PlayerController`는 마우스 포인트 또는 물리 콜라이더를 통해 상호작용할 수 있는 오브젝트를 감지합니다.
- `E` 키 입력으로 상호작용을 수행합니다.

### 📦 2. 아이템 정보 시스템
- `ItemObject`는 `ItemData (ScriptableObject)`를 로드하여 아이템의 이름, 설명, 타입 정보를 제공합니다.
- 마우스를 아이템에 올리면 UI에 해당 정보가 표시됩니다.

### ❤️ 3. 자원 회복 시스템
- `ResourceManager`는 체력(Health), 스태미너(Stamina) 리소스를 관리합니다.
- 소비 아이템을 획득하면 자동으로 적절한 자원을 회복합니다.
  - `ConsumableType.health` → 체력 회복
  - `ConsumableType.stamina` → 스태미너 회복

### 🧠 4. 인터페이스 & 액션 기반 구조
- `IInteractable` 인터페이스를 통해 아이템과의 상호작용을 통일된 방식으로 처리합니다.
- `Action<float>`을 사용하여 자원 회복 처리 로직을 깔끔하게 분리했습니다.
- 인터페이스는 여러 개 상속 가능하여 기능별로 확장성 높은 구조입니다.

---

## 🏗️ 코드 구조

### 📁 Scripts


## 🛠️ 사용 기술

- Unity 2021+ (또는 상위 버전)
- C# with OOP, Interfaces, Delegates
- `InputSystem` (E 키 상호작용)
- `ScriptableObject` 기반 아이템 구조
- `Physics.Raycast`, `OnTriggerEnter` 등 상호작용 감지 방식

---

## ✅ 향후 개선 방향

- 장비(Equipable) 아이템 추가 및 장착 시스템 구현
- 인벤토리 시스템 확장
- UI 애니메이션 개선 및 모바일 대응

---

## 📷 미리보기

> (게임 화면 캡처 또는 GIF 삽입 위치)

---

## 👨‍💻 개발자

- 개발자: 박민수
- 주요 관심사: 게임 개발, 시스템 설계, 인터랙션 UX

---


