using UnityEngine;

// สมมติว่าคลาสเหล่านี้มีอยู่แล้วในโปรเจกต์ของคุณ
// public class Cell : MonoBehaviour { /* ... */ }
// public class Recipe { /* ... */ }
// public class RecipeDatabase : ScriptableObject { /* ... */ } 
// หรือเป็นคลาส C# ธรรมดา

public class MixSystem : MonoBehaviour
{
    // Attributes (ตัวแปร/ฟิลด์)
    // - selectedA: Cell (private)
    [SerializeField] private Cell selectedA;

    // - selectedB: Cell (private)
    [SerializeField] private Cell selectedB;

    // - recipeDB: RecipeDatabase (private)
    [SerializeField] private RecipeDatabase recipeDB;

    // Methods (เมธอด/ฟังก์ชัน)

    // + select(cell: Cell): void (public)
    public void select(Cell cell)
    {
        // ตรรกะการเลือก: เช่น กำหนดให้ selectedA หากว่าง หรือ selectedB หาก selectedA ไม่ว่าง
        if (selectedA == null)
        {
            selectedA = cell;
            Debug.Log($"Selected A: {cell.name}");
        }
        else if (selectedB == null && selectedA != cell)
        {
            selectedB = cell;
            Debug.Log($"Selected B: {cell.name}");
        }
        else
        {
            // หากทั้งคู่ถูกเลือกแล้ว อาจจะยกเลิกการเลือก หรือเริ่มเลือกใหม่
            Debug.Log("Both slots are full. Clearing selection.");
            selectedA = null;
            selectedB = null;
            select(cell); // เลือกเซลล์ปัจจุบันอีกครั้ง
        }
    }

    // + mix(): Recipe (public)
    public Recipe mix()
    {
        if (validateMix())
        {
            // ตรรกะการผสม: ค้นหาสูตรใน recipeDB โดยใช้ selectedA และ selectedB
            Recipe resultRecipe = recipeDB.FindRecipe(selectedA, selectedB);

            // ล้างการเลือกหลังจากผสม
            selectedA = null;
            selectedB = null;

            return resultRecipe;
        }

        // หากการผสมไม่ถูกต้อง
        Debug.LogWarning("Mix operation failed: Selection is invalid.");
        return null;
    }

    // + validateMix(): bool (public)
    public bool validateMix()
    {
        // ตรรกะการตรวจสอบ: ตรวจสอบว่า selectedA และ selectedB ถูกเลือกแล้ว
        // และอาจจะตรวจสอบว่ามีสูตรสำหรับส่วนผสมทั้งสองนี้หรือไม่
        return selectedA != null && selectedB != null;
    }
}