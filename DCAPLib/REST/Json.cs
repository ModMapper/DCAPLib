using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DCAPI.REST
{
    //Json을 사용하기 편리하게 하는 클래스입니다.
    public class Json : IEnumerable<Json> {
        private JsonElement element;
        
        public Json(Stream stream)
            => element = JsonDocument.Parse(stream).RootElement;
        
        private Json(JsonElement e)
            => element = e;
        
        private Json(JsonElement e, string name)
            => (element, Name) = (e, name);
        
        public string Name { get; }
        
        //디버그용...
        //public string Raw => element.GetRawText();
        
        public Json this[int index] {
            get {
                if(element.ValueKind != JsonValueKind.Array)
                    return null;
                if(index < 0 || element.GetArrayLength() <= index)
                    return null;
                return new Json(element[index]);
            }
        }
        
        public Json this[string name] {
            get {
                if(!element.TryGetProperty(name, out var value))
                    return null;
                return new Json(value, name);
            }
        }
        
        public int Length
            => element.ValueKind == JsonValueKind.Array ?
                element.GetArrayLength() : 0;
        
        public bool Contains(string name)
            => element.TryGetProperty(name, out _);
        
        public IEnumerator<Json> GetEnumerator() {
            if(element.ValueKind == JsonValueKind.Array)
                foreach(var i in element.EnumerateArray())
                    yield return new Json(i);
            else if(element.ValueKind == JsonValueKind.Object)
                foreach(var i in element.EnumerateObject())
                    yield return new Json(i.Value, i.Name);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
        
        public static implicit operator decimal(Json value)
            => value.element.GetDecimal();
            
        public static implicit operator string(Json value)
            => value.element.GetString();
        
        public static implicit operator bool(Json value)
            => value.element.GetBoolean();
    }
}