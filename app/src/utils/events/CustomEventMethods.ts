export class CustomEventMethods {
    constructor(private readonly type: string) {
    }

    addListener(callbackFn: EventListenerOrEventListenerObject) {
        window.addEventListener(this.type, callbackFn);
    }

    removeListener(callbackFn: EventListenerOrEventListenerObject) {
        window.removeEventListener(this.type, callbackFn);
    }

    dispatch<T>(eventInitDict?: CustomEventInit<T | null>): void{
        const event = new CustomEvent(this.type, eventInitDict);
        window.dispatchEvent(event);
    }
}