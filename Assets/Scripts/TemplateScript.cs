using UnityEngine;

/**
Template of Script, Coding Conventions
*/

namespace NiceLib {
  public class TemplateScript : MonoBehaviour {
    /**
      Spaces: 2
      EOL: LF
      { Same Line
    */

    #pragma warning disable
    
    string camelCase = "Coding Style";
    string single = "Example of single worded variables";
    const string TEMPLATE_CONST = "const are SCREAMING_CAPS";

    [SerializeField]
    string serialize = "Serialize instead of making it Public";

    #pragma warning enable

    void Start() {
    }

    void Update() {

    }

    private void PascalCase() {

    }
  }
}
