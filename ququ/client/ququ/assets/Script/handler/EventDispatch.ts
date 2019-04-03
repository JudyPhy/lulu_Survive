import { Observer } from "./Observer"

export class EventDispatch {

    private static listeners = {};

    public static register(eventName: string, callback: Function, context: any) {
        let observers: Observer[] = EventDispatch.listeners[eventName];
        if (!observers) {
            EventDispatch.listeners[eventName] = [];
        }
        // console.log("register:", eventName);
        EventDispatch.listeners[eventName].push(new Observer(callback, context));
    }

    public static remove(eventName: string, callback: Function, context: any) {
        let observers: Observer[] = EventDispatch.listeners[eventName];
        if (!observers) return;
        let length = observers.length;
        for (let i = 0; i < length; i++) {
            let observer = observers[i];
            if (observer.compar(context)) {
                observers.splice(i, 1);
                break;
            }
        }
        if (observers.length == 0) {
            delete EventDispatch.listeners[eventName];
        }
    }

    public static fire(eventName: string, ...args: any[]) {        
        let observers: Observer[] = EventDispatch.listeners[eventName];
        if (!observers) return;
        let length = observers.length;
        for (let i = 0; i < length; i++) {
            let observer = observers[i];
            observer.notify(eventName, ...args);
            // console.log("fire:", eventName);
        }
    }

}
